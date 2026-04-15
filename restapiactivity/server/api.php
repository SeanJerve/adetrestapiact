<?php
// =========================================================================
// SERVER: api.php
// PURPOSE: Primary REST API Authentication Endpoint
// DESCRIPTION: This file handles all incoming login requests from our frontends 
//              (C# and Web). It reads the JSON payload, checks the MySQL database 
//              securely, and returns a strict JSON response with HTTP status codes.
// =========================================================================

// --- 1. SETTING COMMUNICATION HEADERS ---
// We explicitly tell any downloading client that this file returns JSON, not HTML.
header("Content-Type: application/json");

// Cross-Origin Resource Sharing (CORS) Configuration
// This allows our javascript Web Client (which might run on a different port) 
// to safely talk to this API without the browser blocking it for security.
header("Access-Control-Allow-Origin: *"); 
header("Access-Control-Allow-Methods: POST, OPTIONS");
header("Access-Control-Allow-Headers: Content-Type");

// Preflight Check: Browsers automatically send an 'OPTIONS' ping before a POST.
// We intercept it and return a 200 OK so the browser knows it's safe to proceed.
if ($_SERVER['REQUEST_METHOD'] === 'OPTIONS') {
    http_response_code(200);
    exit;
}

// --- 2. EXTRACTING FRONTEND DATA ---
// We cannot use standard $_POST because modern REST apps send raw JSON objects.
// We capture the raw input stream and decode it into a usable PHP Array.
$inputJSON = file_get_contents('php://input');
$input = json_decode($inputJSON, true);

// Safely extract the parameters, falling back to null if they aren't provided.
$username = $input['username'] ?? $_POST['username'] ?? null;
$password = $input['password'] ?? $_POST['password'] ?? null;

// --- 3. DATABASE CONFIGURATION ---
// Credentials to connect to the local XAMPP MySQL environment.
$db_host = "localhost";
$db_user = "root";       // Default XAMPP username
$db_pass = "";           // Default XAMPP password
$db_name = "auralis_db"; // Our custom database containing the 'users' table

// Attempting to establish the connection utilizing object-oriented MySQLi
$conn = new mysqli($db_host, $db_user, $db_pass, $db_name);

// If the database is offline or XAMPP isn't running, we kill the script immediately
// and return a 500 Internal Server Error so the frontend knows what broke.
if ($conn->connect_error) {
    echo json_encode(["status" => "error", "message" => "Database connection failed. Did you start MySQL in XAMPP?", "code" => 500]);
    http_response_code(500);
    exit;
}

// We artificially pause the script for 1 second to simulate real-world internet latency.
// This allows our frontends to successfully render their "Loading" animations for the presentation.
sleep(1); 

// --- 4. VALIDATION PIPELINE ---
// Check if the user accidentally pressed login while leaving fields entirely blank.
if (!$username || !$password) {
    echo json_encode(["status" => "error", "message" => "Missing username or password.", "code" => 400]);
    http_response_code(400); // 400 Bad Request
    exit;
}

// --- 5. SECURE DATABASE QUERYING ---
// Instead of injecting the username directly into the SQL (which causes SQL Injection hacks),
// we use "Prepared Statements". We prepare the template, then bind the 's' (string) safely.
$stmt = $conn->prepare("SELECT password, name, role, status FROM users WHERE username = ?");
$stmt->bind_param("s", $username);
$stmt->execute();
$result = $stmt->get_result();

// If the database returns 0 rows, it means the username does not exist.
if ($result->num_rows === 0) {
    echo json_encode(["status" => "error", "message" => "User account not found.", "code" => 404]);
    http_response_code(404); // 404 Not Found
    $stmt->close();
    $conn->close();
    exit;
}

// Extract the user data into a usable PHP array
$userData = $result->fetch_assoc();

// Evaluate the password the user typed against the password stored in the database.
if ($userData['password'] !== $password) {
    echo json_encode(["status" => "error", "message" => "Invalid password. Access denied.", "code" => 401]);
    http_response_code(401); // 401 Unauthorized
    $stmt->close();
    $conn->close();
    exit;
}

// Evaluate the account status. If an admin banned them, reject the login.
if ($userData['status'] === 'locked') {
    echo json_encode(["status" => "error", "message" => "Account is locked. Please contact support.", "code" => 403]);
    http_response_code(403); // 403 Forbidden
    $stmt->close();
    $conn->close();
    exit;
}

// --- 6. SUCCESSFUL AUTHORIZATION ---
// Everything passed! We encode all data into a JSON string and send it back to the client.
// We bundle their specific profile info so the frontend can display their name and role dynamically.
echo json_encode([
    "status" => "success",
    "message" => "Login successful!",
    "userProfile" => [
        "name" => $userData['name'],
        "role" => $userData['role']
    ],
    "code" => 200 // 200 OK
]);

// Clean up memory and destroy the active database connection
$stmt->close();
$conn->close();
?>
