# PieFunds

PieFunds is a .NET 8 Web API for managing user accounts, built with C# 12 and MediatR. It demonstrates clean architecture principles and includes in-memory persistence for easy testing and development.

## Features

- Create a new user (`POST /api/user`)
- Retrieve user details by email (`GET /api/user/{email}`)
- In-memory user repository for demo and tests
- CQRS pattern with MediatR
- Unit tests for user features

## Technologies

- .NET 8
- C# 12
- ASP.NET Core Web API
- MediatR
- xUnit (for tests)

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 or later

### Running the API

1. Clone the repository:
    ```sh
    git clone https://github.com/james-datacom/pie-funds.git
    ```
2. Open the solution in Visual Studio.
3. Set `PieFunds.Api` as the startup project.
4. Run the project (`F5` or `Ctrl+F5`).

### Running Tests

1. Open Test Explorer in Visual Studio.
2. Run all tests in the `PieFunds.Tests` project.

## Project Structure

- `PieFunds.Api` - ASP.NET Core Web API
- `PieFunds.Application` - Application logic, commands, queries, DTOs
- `PieFunds.Infrastructure` - Persistence layer (in-memory repository)
- `PieFunds.Tests` - Unit tests
