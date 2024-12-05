# RiraCRUD

RiraCRUD is a simple CRUD application built using **gRPC** APIs and **Clean Architecture** principles. The project is designed to manage resources efficiently and maintainably, utilizing modern technologies such as **MediatR**, **AutoMapper**, and custom exception handling via interceptors and middleware.

## Features
- **gRPC API**: High-performance communication between client and server using gRPC.
- **Clean Architecture**: Organized structure to ensure scalability and maintainability.
- **MediatR**: Used to handle application logic in a decoupled manner via commands and queries.
- **AutoMapper**: Used to map between DTOs and domain entities automatically.
- **Custom Exception Handling**: Using interceptors and middleware for global error handling.

## Technologies Used
- **ASP.NET Core**
- **gRPC**
- **MediatR**
- **AutoMapper**
- **SQL Server**
- **Entity Framework Core**

## Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/sep96/RiraCRUD.git
    ```

2. Install required dependencies:
    ```bash
    cd RiraCRUD
    dotnet restore
    ```

3. Run the application:
    ```bash
    dotnet run
    ```

## Running Tests

To run the unit tests for the project:

```bash
dotnet test
