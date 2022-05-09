

# Choose your own adventure

Create a back-end for a simple web application which allows a player to choose their own adventure by picking from multiple choices in order to progress to the next set of choices, until they get to one of the endings. You should be able to persist the player's choices and in the end, show the steps they took to get to the end of the game. The front enders need you to build endpoints for 3 pages. 

1. A "Create an Adventure" page which lets creators design the adventure. It's ok to pass a full adventure tree to/from these endpoints.
2. A "Take an Adventure" page where the users can go through the adventure, make their choices, get to the end and persist the path they took.
3. A "My Adventure" page that shows the result of the adventure with highlighted choices that the user has made in their story.

## Technologies

* [ASP.NET Core 6](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0)
* [Entity Framework Core 6](https://docs.microsoft.com/en-us/ef/core/)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)
* [Xunit](https://xunit.net/), [FluentAssertions](https://fluentassertions.com/)
* [Docker](https://www.docker.com/)

## Getting Started

The easiest way to get started is to install the [NuGet package](https://www.nuget.org/packages/Clean.Architecture.Solution.Template) and run `dotnet new ca-sln`:

1. Install the latest [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
2. Clone the repository
3. Navigate to cloned repo path and run `dotnet restore LobsterInk.API` to restore the nuget packages
4. `dotnet build LobsterInk.API` to build the solution
5. `dotnet run --project LobsterInk.API` to launch the back end (ASP.NET Core Web API)
6. Navigate to `https://localhost:5001/swagger/index.html`

### Docker Configuration

1. Build the docker image using `docker-compose build` inside the root folder
2. Run the image using `docker-compose up`
3. Navigate to `https://localhost:5001/swagger/index.html`

To disable Docker in Visual Studio, right-click on the **docker-compose** file in the **Solution Explorer** and select **Unload Project**.

### Database Configuration

The project is configured to use an SQLite database by default. This ensures that all users will be able to run the solution without needing to set up additional infrastructure (e.g. SQL Server).

If you would like to use SQL Server, you will need to update **API/appsettings.json** as follows:

```json
  "UseInMemoryDatabase": false,
  "ConnectionStrings": {
    "DefaultConnection": "Connection_String_to_SQLServer"
  }
```

Verify that the **DefaultConnection** connection string within **appsettings.json** points to a valid SQL Server instance. 

When you run the application the database will be automatically created (if necessary) and the latest migrations will be applied.

## Overview

### Domain

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.

### Infrastructure

This layer contains classes for accessing external resources such as databses, file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

### API

This layer is a ASP.NET Core 6 API. This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. Therefore only *Startup.cs* should reference Infrastructure.

