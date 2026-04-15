# Individual Journal - Member 1

## Name and Role in the Group
**Name:** [Name]
**Role:** Lead Backend Developer (API Server & Database)

## Specific Tasks Performed
My exact role in the project was architecting and developing the server-side logic in our `api.php` file. I was specifically tasked with creating a secure, RESTful authentication endpoint that both our C# application and our web application could safely verify users against. I configured the MySQL Database connection parameters and built the logic pipeline that securely processed JSON login requests into verifiable database actions.

## Code Contributions
I wrote the entirety of the `api.php` logic. First, I established the universal REST standards by utilizing `header("Content-Type: application/json")` so all clients knew how to read the data. I constructed the JSON parser using `file_get_contents('php://input')` which allowed the script to cleanly extract incoming passwords and usernames from frontend `POST` requests. Furthermore, I implemented robust backend security utilizing `mysqli` Prepared Statements (`$stmt->bind_param("s", $username);`) protecting our `auralis_db` from SQL Injection vulnerabilities. Finally, I authored the logic matrix returning distinct error payloads and exact HTTP status codes ranging from `400 Bad Request` to `200 Success` depending on the database logic verification.

## Challenges Encountered
A debilitating challenge during backend implementation was the "Cross-Origin Resource Sharing" (CORS) restriction. Whenever our web client attempted to POST to the API, the browser blocked the request for security reasons. Additionally, I struggled with how the PHP engine halted or crashed completely if incomplete JSON data was submitted by the client (for example, missing a password field entirely).

## How they Contributed to Solving Problems
I solved the server crashing issue by strictly instituting validation conditionals (`if (!$username || !$password)`) directly after decoding the JSON payload, which gracefully ejected a `"status" => "error"` packet rather than triggering a fatal PHP crash. I actively resolved the CORS issue by carefully documenting and inserting the explicit `Access-Control-Allow-Origin: *` strings combined with handling native browser `OPTIONS` preflight checks, creating a perfectly stabilized environment allowing all web fetches to be securely authorized without browser blockages.
