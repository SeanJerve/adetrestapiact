/**
 * =========================================================================
 * WEB CLIENT: app.js
 * PURPOSE: Logic controller for the HTML interface
 * DESCRIPTION: Handles the form submission, prevents page reloading, packages
 *              the credentials into JSON, natively fetches data from api.php, 
 *              and dynamically controls CSS states to display errors or profiles.
 * =========================================================================
 */

// Wait for the HTML document to fully parse before attaching any logic.
document.addEventListener('DOMContentLoaded', () => {
    
    // --- 1. DOM ELEMENT REFERENCES ---
    // We grab physical connections to our HTML tags so Javascript can manipulate them.
    const loginForm = document.getElementById('loginForm');
    const submitBtn = document.getElementById('submitBtn');
    const responseMessage = document.getElementById('responseMessage');
    const loginContainer = document.querySelector('.login-container');
    const profileContainer = document.getElementById('profileContainer');
    const logoutBtn = document.getElementById('logoutBtn');

    // Centralized URL pointing to our PHP API. Must match the XAMPP port.
    const API_URL = 'http://localhost:8000/api.php';

    // --- 2. THE FORM SUBMISSION PIPELINE ---
    // Listen for the exact moment the user clicks the "Login" submit button.
    loginForm.addEventListener('submit', async (e) => {
        // Prevent the browser's default behavior, which is to refresh the entire webpage.
        e.preventDefault(); 
        
        // Extract the raw text the user typed into the input boxes.
        const usernameInput = document.getElementById('username').value;
        const passwordInput = document.getElementById('password').value;

        // Visual Feedback: Turn the submit button into a "loading" state.
        // We disable it so the user can't spam the server with clicks.
        submitBtn.classList.add('loading');
        submitBtn.disabled = true;
        
        // Hide any previous error messages before making a new attempt.
        responseMessage.classList.add('hidden');
        responseMessage.className = 'response-message hidden'; // resets any red colorations

        try {
            console.log(`[NETWORK] Dispatching POST sequence to ${API_URL}`);
            
            // --- 3. FETCH API EXECUTION ---
            // Natively contact the server without needing external libraries like jQuery/Axios.
            // We use 'await' demanding Javascript code to pause here until the network finishes.
            const response = await fetch(API_URL, {
                method: 'POST', // Writing data into the server for verification
                headers: {
                    'Content-Type': 'application/json' // Explicitly announcing JSON formatting
                },
                // Turn our javascript properties into a network-safe JSON text string 
                body: JSON.stringify({
                    username: usernameInput,
                    password: passwordInput
                })
            });

            // Extract that response body and translate it back from JSON into a Javascript Object.
            const data = await response.json();
            console.log("[NETWORK] Server transmission received:", data);

            // --- 4. DATA RENDERING AND DOM MANIPULATION ---
            // If the PHP array specifically outputted 'success'
            if (data.status === 'success') {
                
                // Completely hide the login box from the screen.
                loginContainer.classList.add('hidden');
                
                // Inject the secure profile data we retrieved from the database into the HTML headers.
                document.getElementById('profileName').textContent = data.userProfile.name;
                document.getElementById('profileRole').textContent = data.userProfile.role;
                
                // For a premium UI feel, grab the first letter of their name for a placeholder Avatar 
                document.getElementById('avatarLetter').textContent = data.userProfile.name.charAt(0);
                
                // Reveal the deeply hidden profile dashboard container!
                profileContainer.classList.remove('hidden');
            } else {
                // If it failed (Wrong Password, Missing Name, etc.), inject the PHP error message directly.
                responseMessage.textContent = data.message;
                responseMessage.classList.add('error'); // Trigger the red CSS styling
                responseMessage.classList.remove('hidden'); // Reveal it to the user
            }
            
        } catch (error) {
            // Very important: If the XAMPP server crashes or is offline, the fetch will fatally fail.
            // This catch block prevents the entire website from breaking and warns the user instead.
            console.error('[FATAL] Network routing failure:', error);
            responseMessage.textContent = "CRITICAL: Could not connect to API. Is XAMPP offline?";
            responseMessage.classList.add('error');
            responseMessage.classList.remove('hidden');
            
        } finally {
            // Irrespective of whether the login passed or failed, the action is over.
            // We must unlock the submit button so they can try again if they want to.
            submitBtn.classList.remove('loading');
            submitBtn.disabled = false;
        }
    });

    // --- 5. LOGOUT CONTROLLER ---
    // Attached to the 'Logout' button physically residing on the Profile Container.
    logoutBtn.addEventListener('click', () => {
        // Erase any passwords sitting in the form boxes
        loginForm.reset();
        
        // Reverse the view: Hide the profile container, resurrect the login container.
        profileContainer.classList.add('hidden');
        loginContainer.classList.remove('hidden');
        
        // Make sure no leftover error messages are lingering on the screen.
        responseMessage.classList.add('hidden');
    });
});
