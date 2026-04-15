# Individual Journal - Member 3

## Name and Role in the Group
**Name:** [Name]
**Role:** Web Client Developer (Frontend Logic)

## Specific Tasks Performed
Within our group architecture, my primary domain was constructing the browser-based web client. My tasks involved creating the structured HTML skeleton, rendering a user-friendly interface, and extensively utilizing Vanilla JavaScript to engineer real-time interactions with the backend REST API server. I essentially built the digital bridge allowing organic web users to execute CRUD operations efficiently through an internet browser without requiring any formal .exe installations.

## Code Contributions
I authored the entirety of the frontend logical scripting in `app.js`. I natively utilized the modern browser API, specifically the `Fetch API`, abandoning older `XMLHttpRequest` structures. By utilizing advanced Javascript ES6 features, I wrote `async/await` driven network requests. To actively create interactivity, I generated dynamic DOM (Document Object Model) behaviors. For instance, when the `fetch` pulled the JSON array of records from the database, I wrote `forEach` iteration algorithms to systematically build `<tr>` and `<td>` HTML elements dynamically in real-time, injecting them into the DOM utilizing `.innerHTML` and `appendChild`. Furthermore, I built event listeners appending callbacks to form submissions by invoking `e.preventDefault()` to stop normal HTML page reloading behaviors.

## Challenges Encountered
My major technical challenge was achieving effective "State Synchronization" within the frontend UI. When a user clicked "Delete" or "Update" on a specific row, the API easily processed it in the backend, but the frontend visually struggled. Attempting to manually find and delete individual HTML `<tr>` elements from the DOM after an API call proved highly unstable and prone to glitches if multiple records were adjusted quickly. 

## How they Contributed to Solving Problems
To remediate the DOM instability issue, I implemented a robust, modular approach to screen rendering. Instead of surgically trying to edit raw HTML nodes concurrently during network calls, I developed a universal `loadRecords()` function. Upon any successful mutation—whether it be an `await fetch` containing a POST, PUT, or DELETE request—I simply forced the script to invoke `loadRecords()`. This systematically cleared the existing table and requested a completely fresh slate of data from the database, instantly validating truth and effectively eliminating 100% of the UI de-synchronization glitches. This resulted in a seamlessly snappy user experience.
