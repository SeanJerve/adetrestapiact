using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace CSharpClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private const string API_URL = "http://localhost:8000/api.php";

        // C# 5 does not support async Main, so we wrap it
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            Console.Title = "Secure Authentication Gateway - CLI Client";
            Console.Clear();
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==================================================");
            Console.WriteLine("||           AURALIS SECURE GATEWAY             ||");
            Console.WriteLine("||           v1.0.0 - CLI Edition               ||");
            Console.WriteLine("==================================================");
            Console.ResetColor();
            Console.WriteLine("\nEstablishing secure connection to REST API...");
            Thread.Sleep(500); 
            
            while (true)
            {
                Console.WriteLine("\n[ PLEASE AUTHENTICATE ]");
                
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("  Username > ");
                string username = Console.ReadLine();
                
                Console.Write("  Password > ");
                string password = ReadPassword();
                
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n[!] Error: Username and password cannot be empty.");
                    continue;
                }

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("\n[~] Transmitting credentials securely ");
                
                for(int i = 0; i < 3; i++) {
                    Console.Write(".");
                    Thread.Sleep(300);
                }
                Console.WriteLine();
                
                await AuthenticateUser(username, password);
                
                Console.ResetColor();
                Console.WriteLine("\nPress Enter to try again or 'q' to quit...");
                if (Console.ReadLine().ToLower() == "q") break;
            }
        }
        
        static async Task AuthenticateUser(string username, string password)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var payload = new Dictionary<string, string>
                {
                    { "username", username },
                    { "password", password }
                };
                string jsonPayload = serializer.Serialize(payload);
                
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(API_URL, content);
                string responseBody = await response.Content.ReadAsStringAsync();
                
                var dict = serializer.Deserialize<Dictionary<string, object>>(responseBody);
                
                Console.WriteLine("\n--- SERVER RESPONSE ---");
                
                // Protect against null responses
                if (dict != null && dict.ContainsKey("status"))
                {
                    string status = dict["status"].ToString();
                    string message = dict.ContainsKey("message") ? dict["message"].ToString() : "";
                    
                    if (status == "success")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("[SUCCESS] " + message);
                        
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
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("[DENIED] " + message);
                        string code = dict.ContainsKey("code") ? dict["code"].ToString() : "Unknown";
                        Console.WriteLine("  Error Code: " + code);
                    }
                }
                else
                {
                     Console.WriteLine(responseBody);
                }
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[FATAL] Error connecting to API: " + ex.Message);
                Console.WriteLine("Make sure the PHP server is running on localhost:8000.");
                Console.ResetColor();
            }
        }

        static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return password;
        }
    }
}
