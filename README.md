# ZineCo NewsAgent Matcher

A .NET 8 application for validating ZineCo newsagents against multiple chain sources using a pluggable matching strategy.

---

## 📋 Project Overview

This solution integrates:
- Multiple chain APIs (SUP, ADV, NIW)
- Location and name-based matching algorithms
- Factory pattern for matcher resolution
- Resilience with retry and timeout for API calls
- Unit tests for services, APIClient, and matchers

---

## 🛠️ Tech Stack

- .NET 8
- Minimal API
- Polly via `Microsoft.Extensions.Http.Resilience`
- xUnit, Moq for unit testing
- Dependency Injection (built-in DI)
- Logging via Microsoft.Extensions.Logging

---

## 🚀 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) or VS Code

### Step to Run Application
- Unzip the Application
  ## Run The Application
  - dotnet run --project ZinecoMatcher
  - Open this address in a browser
  ### http://localhost:5282/swagger/index.html (Please check once if it is running on the 5282 port)
## Run Unit Test
- Open bash/cmd on the root Zip Folder where there are ZincoMatcher and ZincoMatcherTest, both
- Run the command
- dotnet test ZincoMatcherTest

### APIs Exposed and steps to test
- Swagger UI implemented to test the Endpoints
  - ## http://localhost:5282/getAllChainAgentValidation
      - This is a GET API with no parameters. In this API, I retrieved the Zineco Agent List from the API and did a matching and
         Validate that the agent data is correct and return a list of Validation results with valid status and messages.
   - ## http://localhost:5282/getChainAgentValidation
       - This is a POST api that takes Zineco agent data and validates with ChainId if the agent data is correct. After matching and validation, following the defined algorithm,
       - Returned validation result with valid status and message.

### Design Patterns used in the project

| Pattern        | Purpose                                          |
| -------------- | ------------------------------------------------ |
| **Factory**    | Selects the appropriate matcher based on ChainId |
| **Strategy**   | Different matching logic per chain               |
| **Resilience** | Retry, Timeout, Circuit Breaker on API calls     |

- A combination of Factory and Strategy design patterns follows the Open-Closed Principle of SOLID.
- This pattern implementation will help to add a new Chain in the future with easy and minimal code changes.

### AI tools Used
 ## Github Copilot used to implement the method for geo-location distance calculation and some regex implementation. Besides this, Copilot suggestions are used for the Unit test implementation.
 In case of unit test, I defined the test cases and setups, and took the following suggestions from Copilot.
 ## Used ChatGPT in some places also. Some prompt was
  - How to mock HttpClient (I was facing difficulties in mocking HttpClient directly).
  - Took the idea about some Microsoft resiliency extension property best practices to implement in the project since I had to call multiple third-party API.

### Future Improvement can be done
- Now all the APIs are public. I can integrate the Authorization and Authentication system here.
- Since there was no big list of data and proper swagger was missing, I retrieved all the data at once and processed. I can implement pagination here to retrieve and process.
