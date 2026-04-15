# Individual Journal - Member 2

## Name and Role in the Group
**Name:** [Name]
**Role:** C# Desktop Client Developer

## Specific Tasks Performed
My unique responsibility within our group was engineering the desktop application client using C# (`Program.cs`). My primary objective was to build a secure, command-line interface (CLI) application capable of securely networking with the PHP REST API to execute the remote authentication sequences seamlessly without needing an organic web browser.

## Code Contributions
I structured and meticulously developed the core networking loop utilizing the C# `System.Net.Http.HttpClient` library. I authored the `AuthenticateUser` method specifically converting user console string inputs into structured Dictionary packets using internal `JavaScriptSerializer.Serialize()` methodologies, successfully mocking standard HTTP JSON POST request bodies. When the server successfully responded, my code deserialized the raw network strings (`ReadAsStringAsync()`) back into C# variables. I also engineered the primary CLI user interface—managing `Console.ForegroundColor` states, and specifically implementing a custom `ReadPassword()` method that actively masked users' keyboard strokes with asterisks (`*`) for security.

## Challenges Encountered
Developing network code within a synchronous Console environment rapidly produced architectural challenges. If the server lagged during a request, the entire console application would freeze permanently. Additionally, handling complex JSON arrays in C# logic required dynamically casting untyped string outputs to readable dictionaries, which often threw `NullReferenceExceptions` when the server arbitrarily returned distinct error objects lacking "userProfile" fields.

## How they Contributed to Solving Problems
I bypassed the synchronous freezing completely by refactoring the `Main` thread into an asynchronous `MainAsync().Wait()` task pool, implementing `async/await` methodology for the `client.PostAsync()` functions to ensure the network operations processed sequentially without choking the CPU. I actively prevented JSON parsing exceptions by utilizing `dict.ContainsKey()` validity checks prior to executing the UI rendering, seamlessly handling the diverse error states—whether the user was met with an "Account Locked" or "Wrong Password" string depending entirely on the API's distinct return configurations.
