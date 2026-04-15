<?php
// C:\Users\seanjerve\OneDrive\Desktop\ADET\restapiactivity\server\api.php

header("Content-Type: application/json");
header("Access-Control-Allow-Origin: *"); // Very important for the web client
header("Access-Control-Allow-Methods: POST, OPTIONS");
header("Access-Control-Allow-Headers: Content-Type");

if ($_SERVER['REQUEST_METHOD'] === 'OPTIONS') {
    http_response_code(200);
    exit;
}

// Read the raw POST data
$inputJSON = file_get_contents('php://input');
$input = json_decode($inputJSON, true);

// If not JSON, maybe form data
$username = $input['username'] ?? $_POST['username'] ?? null;
$password = $input['password'] ?? $_POST['password'] ?? null;

// Database Connection (XAMPP Default)
$db_host = "localhost";
$db_user = "root";       // Default XAMPP username is "root"
$db_pass = "";           // Default XAMPP password is empty
$db_name = "auralis_db"; // The database we will create in phpMyAdmin

// Attempt to connect to MySQL database
$conn = new mysqli($db_host, $db_user, $db_pass, $db_name);

if ($conn->connect_error) {
    echo json_encode(["status" => "error", "message" => "Database connection failed. Did you start MySQL in XAMPP?", "code" => 500]);
    http_response_code(500);
    exit;
}

// Random delay to simulate real network request and give the client time to show loading animations
sleep(1); 

// Validation Logic
if (!$username || !$password) {
    echo json_encode(["status" => "error", "message" => "Missing username or password.", "code" => 400]);
    http_response_code(400);
    exit;
}

// 1. Check if user exists (using Prepared Statements to prevent SQL Injection!)
$stmt = $conn->prepare("SELECT password, name, role, status FROM users WHERE username = ?");
$stmt->bind_param("s", $username);
$stmt->execute();
$result = $stmt->get_result();

if ($result->num_rows === 0) {
    echo json_encode(["status" => "error", "message" => "User account not found.", "code" => 404]);
    http_response_code(404);
    $stmt->close();
    $conn->close();
    exit;
}

$userData = $result->fetch_assoc();

// 2. Validate Password
if ($userData['password'] !== $password) {
    echo json_encode(["status" => "error", "message" => "Invalid password. Access denied.", "code" => 401]);
    http_response_code(401);
    $stmt->close();
    $conn->close();
    exit;
}

// 3. Check Account Status
if ($userData['status'] === 'locked') {
    echo json_encode(["status" => "error", "message" => "Account is locked. Please contact support.", "code" => 403]);
    http_response_code(403);
    $stmt->close();
    $conn->close();
    exit;
}

// 4. Success! Send back the user profile
echo json_encode([
    "status" => "success",
    "message" => "Login successful!",
    "userProfile" => [
        "name" => $userData['name'],
        "role" => $userData['role']
    ],
    "code" => 200
]);

$stmt->close();
$conn->close();
?>
