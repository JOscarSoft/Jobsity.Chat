# Jobsity Chat

A chat bot web application focused on the back-end code.

### Completed bonus task

- Authentication with IdentityServer
- Command errors are handled by chat Administrator

### Libraries

- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/)
- [IdentityServer](https://www.nuget.org/packages/Microsoft.AspNetCore.Identity.EntityFrameworkCore)
- [.NET 5](https://docs.microsoft.com/pt-br/dotnet/core/dotnet-five)
- [RabbitMQ](https://www.rabbitmq.com/)

## Getting Started

### Prerequisites

- .NET 5 Build Sdk && .NET 5 Runtime
- Microsoft SQL Server 2017
- RabbitMQ

### Run

1. Start your RabbitMQ Server + MSSQL Server 2017
2. Update the appSettings in Jobsity.Chat.App/appsettings.json and Jobsity.Chat.Bot/appsettings.json
3. In the root folder, run

```
dotnet restore
dotnet build
```

4. Then, start rabbit bot with

```
cd Jobsity.Chat.Bot
dotnet run
```

5. Run the web app (Jobsity.Chat.App)
6. Create your account
7. Start chatting

### Tests

On the project root, run this command to run all tests

```
cd Jobsity.Chat.Tests
dotnet test
```
