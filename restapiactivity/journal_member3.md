# Individual Journal - Member 3

## Name and Role in the Group
**Name:** [Name]
**Role:** Web Client Developer (Frontend Logic)

## Specific Tasks Performed
My primary focus was designing and engineering the functional Javascript logic dictating how our Web HTML interface securely integrated with the backend API. I essentially programmed the client-side mechanism (`app.js`) that physically collected user inputs in the web browser, converted them into exact JSON packets, transmitted them securely across the network to our PHP server, and dynamically shifted the DOM based on the returned responses.

## Code Contributions
I constructed the core network interactions utilizing the native Javascript `Fetch API` within `app.js`. By wiring custom javascript event listeners spanning the `document.addEventListener('DOMContentLoaded', ...)` hook, I captured form submissions while actively deploying `e.preventDefault()` to stop archaic page reloading. My script wrapped the username and password nodes utilizing `JSON.stringify()`, declaring exact `POST` methods against the `http://localhost:8000` port. I additionally developed the display manipulations: successfully extracting data objects utilizing `await response.json()`, and mapping them onto the profile UI nodes (`profileName.textContent`, `avatarLetter`). 

## Challenges Encountered
Navigating "Promises" within the Javascript networking pipeline proved remarkably difficult. Because networks operate externally, the script attempted to populate the user profile screen *before* the server technically finished responding, printing `undefined` errors across the DOM. Furthermore, trying to cleanly alternate elements, removing the login view to expose the "Profile Overview" view after successful authorization constantly presented glitchy, misaligned visual elements.

## How they Contributed to Solving Problems
I effectively solved the race-condition Promise timing errors by fully incorporating `async` and `await` directives around the network calls preventing the javascript stack from transitioning screens until the HTTP request legitimately closed. For the UI transition bugs, rather than deleting and redrawing raw HTML nodes, I standardized a rigid class-toggling solution utilizing `.classList.add('hidden')` and `.classList.remove('hidden')`, seamlessly hiding the login containers whilst executing clean, perfectly drawn "Welcome Profile" displays immediately confirming authentication.
