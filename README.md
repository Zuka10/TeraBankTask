    User Account Management System
This project is a User Account Management System built with ASP.NET Core, utilizing MSSQL, Entity Framework Core, CQRS, MediatR, and JWT for authorization. The system allows users to register, authenticate, and perform financial operations such as deposits, withdrawals, and transfers. The application exposes these functionalities through a RESTful API.

    Project Structure
The project follows the principles of Clean Architecture and CQRS (Command Query Responsibility Segregation) to ensure separation of concerns and maintainability:

* Domain: Contains the core business logic and entities.
* Application: Contains the application logic, including commands, queries, handlers, and interfaces.
* Infrastructure: Contains the implementation details for data access, external services, and other infrastructure concerns.
* API: Contains the controllers and middleware to handle client requests and responses.
  
      Technologies Used
* ASP.NET Core
* Entity Framework Core
* MSSQL
* CQRS
* MediatR
* REST API
* JWT (JSON Web Tokens) for authentication and authorization
* FluentValidation
