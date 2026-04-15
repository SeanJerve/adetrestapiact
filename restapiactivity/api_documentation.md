# REST API Documentation - Authentication Service

## Overview
This API provides a secure Authentication backend service for validating user credentials against the `auralis_db` database. 
The server uses **PHP** and MySQL (`mysqli`) utilizing Prepared Statements to protect against SQL Injection. It returns standard structured JSON responses containing operation statuses and HTTP status codes to allow frontends (Web and C#) to react accordingly.

### Base URL
`http://localhost/api.php` *(For local testing environments)*

## Communication Protocol
- **Format:** All requests and responses use `application/json`.
- **CORS:** Cross-Origin Resource Sharing is enabled natively via `Access-Control-Allow-Origin: *` to prevent browser blockages on the web client.
- **Methods Allowed:** `POST`, `OPTIONS` (Preflight checking)

---

## 1. User Authentication (Login)
Validates a user's credentials against the database. If successful, returns basic non-sensitive profile information to initialize the client application.

- **Method:** `POST`
- **Endpoint:** `/api.php`
- **Headers:** `Content-Type: application/json`
- **Request Body Configuration:**
```json
{
  "username": "your_username",
  "password": "your_password"
}
```

### Success Responses

**HTTP 200 OK - Successful Login**
Returned when the username and password match.
```json
{
  "status": "success",
  "message": "Login successful!",
  "userProfile": {
    "name": "User's Full Name",
    "role": "admin"
  },
  "code": 200
}
```

### Error Responses

**HTTP 400 Bad Request - Missing Credentials**
Returned if the client payload is empty or missing required fields.
```json
{
  "status": "error",
  "message": "Missing username or password.",
  "code": 400
}
```

**HTTP 404 Not Found - User does not exist**
Returned when the username string is not found in the users table.
```json
{
  "status": "error",
  "message": "User account not found.",
  "code": 404
}
```

**HTTP 401 Unauthorized - Incorrect Password**
Returned when the username exists, but the password provided does not match.
```json
{
  "status": "error",
  "message": "Invalid password. Access denied.",
  "code": 401
}
```

**HTTP 403 Forbidden - Account Locked**
Returned when the user's `status` field in the database is set to `locked`.
```json
{
  "status": "error",
  "message": "Account is locked. Please contact support.",
  "code": 403
}
```

**HTTP 500 Internal Server Error - Database Offline**
Returned if the PHP script cannot successfully execute the MySQLi connection string.
```json
{
  "status": "error",
  "message": "Database connection failed. Did you start MySQL in XAMPP?",
  "code": 500
}
```
