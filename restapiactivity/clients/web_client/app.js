document.addEventListener('DOMContentLoaded', () => {
    const loginForm = document.getElementById('loginForm');
    const submitBtn = document.getElementById('submitBtn');
    const responseMessage = document.getElementById('responseMessage');
    const loginContainer = document.querySelector('.login-container');
    const profileContainer = document.getElementById('profileContainer');
    const logoutBtn = document.getElementById('logoutBtn');

    // API URL - this must match where your PHP server is running
    const API_URL = 'http://localhost:8000/api.php';

    loginForm.addEventListener('submit', async (e) => {
        e.preventDefault(); // Prevent page reload
        
        const usernameInput = document.getElementById('username').value;
        const passwordInput = document.getElementById('password').value;

        // UI Loading State
        submitBtn.classList.add('loading');
        submitBtn.disabled = true;
        responseMessage.classList.add('hidden');
        responseMessage.className = 'response-message hidden'; // reset classes

        try {
            console.log(`Sending POST request to ${API_URL}`);
            
            // Fetch API to consume the REST server
            const response = await fetch(API_URL, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    username: usernameInput,
                    password: passwordInput
                })
            });

            const data = await response.json();
            console.log("Response received:", data);

            if (data.status === 'success') {
                // Success animation and show profile
                loginContainer.classList.add('hidden');
                
                // Populate profile data
                document.getElementById('profileName').textContent = data.userProfile.name;
                document.getElementById('profileRole').textContent = data.userProfile.role;
                document.getElementById('avatarLetter').textContent = data.userProfile.name.charAt(0);
                
                profileContainer.classList.remove('hidden');
            } else {
                // Show Error
                responseMessage.textContent = data.message;
                responseMessage.classList.add('error');
                responseMessage.classList.remove('hidden');
            }
        } catch (error) {
            console.error('Error connecting to API:', error);
            responseMessage.textContent = "Could not connect to the server. Is api.php running on http://localhost:8000?";
            responseMessage.classList.add('error');
            responseMessage.classList.remove('hidden');
        } finally {
            // Remove loading state
            submitBtn.classList.remove('loading');
            submitBtn.disabled = false;
        }
    });

    logoutBtn.addEventListener('click', () => {
        // Reset and go back to login
        loginForm.reset();
        profileContainer.classList.add('hidden');
        loginContainer.classList.remove('hidden');
        responseMessage.classList.add('hidden');
    });
});
