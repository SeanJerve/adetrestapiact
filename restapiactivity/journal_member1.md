# Individual Journal - Member 1

## Name and Role in the Group
**Name:** [Name]
**Role:** Lead Backend Developer (API Server & Database)

## Specific Tasks Performed
My primary responsibility was architecting and developing the server-side logic for our group project. I was tasked with building a RESTful API using raw PHP, which served as the central hub of communication for both the web and C# desktop clients. My duties also included configuring the database connections, establishing the execution of the CRUD (Create, Read, Update, Delete) operations, and ensuring our API returned clean, reliable JSON responses under varying network conditions. 

## Code Contributions
I wrote the core functionality within `api.php`. Rather than utilizing an external PHP framework like Laravel, I engineered a raw server-side router utilizing `$_SERVER['REQUEST_METHOD']`. This routed incoming `GET`, `POST`, `PUT`, and `DELETE` requests to their corresponding database operations. I implemented the `PDO` connection to securely interface with the database. Furthermore, I authored the data parsing sequence, wherein I used `file_get_contents("php://input")` to digitally capture the incoming JSON string arrays from the client payloads and executed `json_decode` to turn them into usable PHP variables. I also manually structured the outgoing server headers specifically establishing `header("Content-Type: application/json")` to validate the communications network.

## Challenges Encountered
During integration testing with the web frontend, we immediately encountered strict Cross-Origin Resource Sharing (CORS) roadblocks. Because the web client and API were operating technically on distinct ports/origins on the localhost environment, the web browser was blocking our frontend `fetch()` API calls for security reasons. Furthermore, handling cases where incoming JSON payloads were empty or malformed would occasionally cause fatal PHP runtime crashes which disrupted the testing phase for other members.

## How they Contributed to Solving Problems
To remediate the debilitating CORS roadblock, I rigorously investigated origin policies and successfully integrated `Access-Control-Allow-Origin: *` strings into our headers. Crucially, I adapted the server to detect and handle HTTP `OPTIONS` requests commonly referred to as "preflight checks", which satisfied browser security mechanics. I also enhanced the stability of the entire server by fortifying our logic with `try-catch` blocks and input sanitization, actively verifying checking if required parameters existed (e.g., checking if `id` is present before a `DELETE` request). This problem-solving directly enabled the rest of the team to comfortably build responsive clients without worrying about server crashes.
