using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Web.Script.Serialization; // Requires System.Web.Extensions assembly

namespace CSharpClient
{
    // =========================================================================
    // C# CLIENT: Program.cs
    // PURPOSE: Core command-line Logic Application
    // DESCRIPTION: Acts as a Desktop Client. Secures user input from the console,
    //              serializes it securely into JSON, executes an asynchronous 
    //              HTTP POST directly to our PHP server, and outputs the status.
    // =========================================================================
    class Program
    {
        // Global HttpClient instance to manage all network operations safely
        private static readonly HttpClient client = new HttpClient();
        
        // Centralized URL pointing to our PHP API. Must match the XAMPP port.
        private const string API_URL = "http://localhost:8000/api.php";

        // Older framework architectures do not naively support an `async Main` loop.
        // We establish a synchronous Main method but force it to Wait() for an Async task.
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            // --- 1. AESTHETIC CLI INITIALIZATIONS ---
            Console.Title = "Secure Authentication Gateway - CLI Client";
            Console.Clear();
            
            // Simulating a premium, immersive CLI identity 
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==================================================");
            Console.WriteLine("||           AURALIS SECURE GATEWAY             ||");
            Console.WriteLine("||           v1.0.0 - CLI Edition               ||");
            Console.WriteLine("==================================================");
            Console.ResetColor();
            Console.WriteLine("\nEstablishing secure connection to REST API...");
            
            // Brief visual pause to process the connection environment
            Thread.Sleep(500); 
            
            // --- 2. INFINITE APPLICATION LOOP ---
            // Keep the application running so the user can continually make attempts
            while (true)
            {
                Console.WriteLine("\n[ PLEASE AUTHENTICATE ]");
                
                // Read the username string directly from the physical keyboard input
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("  Username > ");
                string username = Console.ReadLine();
                
                // Read the password securely using our custom masking method below
                Console.Write("  Password > ");
                string password = ReadPassword();
                
                // Local Validation: Prevent empty packets from wasting the server's time
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n[!] Error: Username and password cannot be empty.");
                    continue; // Skip processing and restart the loop
                }

                // Visual Feedback Animation: Simulating network encryption
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("\n[~] Transmitting credentials securely ");
                for(int i = 0; i < 3; i++) {
                    Console.Write(".");
                    Thread.Sleep(300); // 300 millisecond pauses between asterisks
                }
                Console.WriteLine();
                
                // Safely hand over the credentials to our async network orchestrator
                await AuthenticateUser(username, password);
                
                Console.ResetColor();
                Console.WriteLine("\nPress Enter to try again or 'q' to quit...");
                if (Console.ReadLine().ToLower() == "q") break; // Safely terminate the program
            }
        }
        
        // --- 3. THE CORE NETWORK LOGIC ROUTINE ---
        static async Task AuthenticateUser(string username, string password)
        {
            try
            {
                // Instantiate the object serializer to bypass needing heavy external packages like JSON.NET
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                
                // Structure our C# strings into a standard Key-Value Dictionary object mapping
                var payload = new Dictionary<string, string>
                {
                    { "username", username },
                    { "password", password }
                };
                
                // Irreversibly transfigure the C# Object into an absolute standard JSON string payload
                string jsonPayload = serializer.Serialize(payload);
                
                // Load the payload into physical Network Content, explicitly tagging it identically 
                // to our PHP server's header "application/json" UTF-8 format.
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                // Execute the Asynchronous POST action. Await forces the timeline to patiently pause 
                // preventing the CPU from violently locking the console interface window.
                HttpResponseMessage response = await client.PostAsync(API_URL, content);
                
                // Download the raw mathematical response data completely as a massive string
                string responseBody = await response.Content.ReadAsStringAsync();
                
                // Decode the PHP server's returned JSON back into generic C# object dictionaries
                var dict = serializer.Deserialize<Dictionary<string, object>>(responseBody);
                
                Console.WriteLine("\n--- SERVER RESPONSE ---");
                
                // --- 4. RESPONSE RENDERING & SECURITY VALIDATION ---
                // We use `.ContainsKey` actively preventing NullReferenceExceptions if the API changes
                if (dict != null && dict.ContainsKey("status"))
                {
                    string status = dict["status"].ToString();
                    
                    // Assign message if present, otherwise default to a blank string
                    string message = dict.ContainsKey("message") ? dict["message"].ToString() : "";
                    
                    // Route the application behavior dependent exclusively on the backend REST Status flag
                    if (status == "success")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("[SUCCESS] " + message);
                        
                        // Extracting complex nested Javascript objects (The Profile Sub-Array)
                        if (dict.ContainsKey("userProfile"))
                        {
                            var profile = (Dictionary<string, object>)dict["userProfile"];
                            string name = profile.ContainsKey("name") ? profile["name"].ToString() : "";
                            string role = profile.ContainsKey("role") ? profile["role"].ToString() : "";
                            
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("  Welcome  : " + name);
                            Console.WriteLine("  Role     : " + role);
                            Console.WriteLine("  Clearance: GRANTED");
                        }
                    }
                    else // The user failed login, or their account was locked, etc.
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("[DENIED] " + message);
                        
                        // Rendering the exact HTTP server status codes we programmed in PHP (e.g. 403, 404, 401)
                        string code = dict.ContainsKey("code") ? dict["code"].ToString() : "Unknown";
                        Console.WriteLine("  Error Code: " + code);
                    }
                }
                else
                {
                     // Fallback mechanism: If the server crashed and dumped PHP text instead of JSON
                     Console.WriteLine(responseBody);
                }
                Console.ResetColor(); // Always normalize color palettes preventing bleed
            }
            catch (Exception ex)
            {
                // Deep Network Level Failure Trap (e.g. The server is strictly unreachable offline)
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[FATAL] Error connecting to API: " + ex.Message);
                Console.WriteLine("Make sure the PHP server is running on localhost:8000.");
                Console.ResetColor();
            }
        }

        // --- 5. UTILITY: ADVANCED CONSOLE PASSWORD MASKING ---
        // A custom module intercepting raw keystrokes globally shielding passwords from sight
        static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true); // 'true' actively suppresses the key from rendering
                
                // If the key is valid, record it and output an asterisk artificially
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                // Logical backspace deletion targeting both the hidden string and the console output
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);
            
            Console.WriteLine(); // Trigger newline universally mimicking standard submission
            return password;
        }
    }
}
