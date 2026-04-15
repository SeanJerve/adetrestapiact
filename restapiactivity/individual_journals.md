# Part 4: Individual Journals

*Note: Replace `[Name]` with the actual names of your group members. You can adjust the roles as needed if they differ from what is outlined below.*

---

## Member 1: [Name] - Backend Developer (API Server)
*   **Name and role in the group:** [Name], Lead Backend Developer
*   **Specific tasks performed:** I was responsible for designing and implementing the REST API server using PHP and managing the core data flow. My focus was ensuring the API endpoints operated securely and efficiently handled requests.
*   **Code contributions:** I wrote the core logic in `api.php`. I implemented the routing mechanism to handle `GET`, `POST`, `PUT`, and `DELETE` HTTP methods. I also contributed the logic to parse incoming `php://input` streams for JSON payloads and ensure all API responses were properly formatted with the correct `Content-Type: application/json` headers and HTTP status codes. 
*   **Challenges encountered:** One significant challenge was implementing Cross-Origin Resource Sharing (CORS) properly. Initially, the Web Client was blocked by the browser from accessing our API due to security restrictions.
*   **How they contributed to solving problems:** I researched and implemented the required `Access-Control-Allow-Origin` and preflight `OPTIONS` headers in the PHP script. This unblocked the frontend development and established a reliable, stable server environment for both of our client applications to consume.

---

## Member 2: [Name] - C# Desktop Client Developer
*   **Name and role in the group:** [Name], C# Client Developer
*   **Specific tasks performed:** My primary responsibility was developing the desktop client using C# and Windows Forms. I built a graphical interface that allowed users to perform CRUD operations by integrating with the PHP REST API.
*   **Code contributions:** I designed the Windows Forms UI and implemented the network logic using `HttpClient` to send asynchronous HTTP requests. I wrote the code to serialize and deserialize JSON responses into C# objects and map that data directly onto a `DataGridView` for the user to see and interact with.
*   **Challenges encountered:** A major challenge was managing network latency without freezing the application. Initially, synchronous HTTP calls would cause the desktop UI to lock up while waiting for the server to reply.
*   **How they contributed to solving problems:** I learned and implemented the `async/await` asynchronous programming pattern across all API-calling methods. This kept the UI highly responsive and smooth during network operations, providing a professional feel to our desktop application.

---

## Member 3: [Name] - Web Client Developer
*   **Name and role in the group:** [Name], Web Client Developer
*   **Specific tasks performed:** I built the browser-based client for our system, handling the frontend application logic, API integration, and DOM manipulation using HTML and Vanilla JavaScript.
*   **Code contributions:** I developed `app.js`, utilizing the native browser `Fetch API` to interact with our PHP server. I wrote the functions to dynamically generate HTML table rows representing our data records. I also created the asynchronous functions that capture user input from the HTML forms, construct a JSON payload, and send it to the server to create or update records.
*   **Challenges encountered:** Keeping the frontend UI synchronized with the backend database was difficult. When a user updated or deleted an item, the visual table needed to reflect the change immediately without requiring a clunky full-page reload.
*   **How they contributed to solving problems:** I structured the JavaScript to easily re-fetch and re-render the data table upon any successful state mutation. I also added client-side validation to ensure forms couldn't be submitted with empty fields, which prevented bad data from reaching the server APIs.

---

## Member 4: [Name] - UI/UX & Integration Specialist
*   **Name and role in the group:** [Name], UI/UX Designer & Integration Specialist
*   **Specific tasks performed:** I focused on the visual design aesthetics and ensuring that the integration between both frontends and the backend was seamless. My role was to make the application look professional and ensure the user experience was highly intuitive.
*   **Code contributions:** I authored `style.css` for the Web Client, implementing a modern, sleek design utilizing CSS Flexbox, custom color variables, and transition animations (like hover effects on buttons). I also contributed to styling the C# desktop application interface, ensuring visual consistency between the Web and Windows app platforms.
*   **Challenges encountered:** Making the web application fully responsive so it looked excellent on both desktop monitors and smaller screens required extensive CSS fine-tuning and debugging layout breakpoints. 
*   **How they contributed to solving problems:** I implemented a robust styling methodology utilizing CSS Grid and Flexbox, which solved the layout-breaking issues. By introducing clear visual feedback—such as disabling buttons during load times and styling error messages in red—I drastically improved the usability and overall polish of the clients.

---

## Member 5: [Name] - QA Tester & Documentation Specialist
*   **Name and role in the group:** [Name], QA Tester & Documentation Specialist
*   **Specific tasks performed:** My role was to test the API and client integrations rigorously to find and fix bugs, and to compile all necessary project documentation, ensuring we met all the academic grading rubrics.
*   **Code contributions:** I created the testing environments and used tools like Postman to validate the API endpoints completely independently of the frontends. I also contributed minor error-handling logic in the backend to catch edge cases (like when a client sends incomplete JSON data).
*   **Challenges encountered:** Complex bugs would sometimes appear unexpectedly, and it was often difficult to track down whether the bug originated from a malformed request in the C# app, the Web app, or a logic error in the PHP routing.
*   **How they contributed to solving problems:** By systematically testing the backend API independently, I was able to successfully isolate server-side bugs from client-side bugs. I logged the issues I found and worked with the developers to patch the vulnerabilities. My documentation efforts ensured that our group's technical work was accurately recorded, formatted, and ready for our final submission.
