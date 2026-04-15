# Individual Journal - Member 2

## Name and Role in the Group
**Name:** [Name]
**Role:** C# Desktop Client Developer

## Specific Tasks Performed
My distinct role within the group was to design and develop the desktop application client using C# and Windows Forms (.NET). My task was ensuring we had a functional, compiled application capable of seamlessly communicating over the network with the centralized PHP REST API. I was directly responsible for implementing the desktop UI layouts, assigning programmatic logic to UI buttons, and structuring the classes necessary to consume the remote JSON data streams effectively.

## Code Contributions
I developed the primary application entry and layouts in `Program.cs` and the respective Forms components. The majority of my technical contribution revolved around implementing the `System.Net.Http.HttpClient` library. I authored methods to systematically fire off `GET`, `POST`, `PUT`, and `DELETE` requests directly mimicking the user’s actions on the interface. To convert the stringified network JSON texts into usable arrays, I actively integrated `System.Text.Json` to dynamically deserialize server responses into generic list classes in C#. I then securely databound these internal lists to a visual `DataGridView`, dynamically generating interactive cells and rows. 

## Challenges Encountered
The most significant hurdle occurred early in development: Application freezing. Whenever an API request was dispatched, the entire Windows Forms UI thread would artificially lock up (freeze) until the PHP server successfully replied. If the server experienced latency, the desktop app gave the impression it had crashed entirely. Additionally, parsing JSON into strictly typed C# object classes posed challenges whenever the API data schema shifted slightly, throwing unhandled conversion exceptions. 

## How they Contributed to Solving Problems
I solved the application UI freezing problem by undergoing a comprehensive refactoring of all my network functions, fully converting my API calling methods to utilize modern `async` and `await` asynchronous programming patterns. This allowed the HTTP requests to run safely on background threads, maintaining a smooth, unblocked user interface at all times. To resolve the parsing crashes, I implemented defensive programming checks utilizing `EnsureSuccessStatusCode()` to catch API errors gracefully. If the backend hit a `404 Not Found` or `400 Bad Request`, my software would intercept the error and cast a safe `MessageBox` to the user rather than shutting down the program, significantly increasing client reliability.
