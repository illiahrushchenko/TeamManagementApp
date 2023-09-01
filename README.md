# TeamManagementApp
TeamManagementApp is an API designed for creating and managing boards, tables, and cards, intended for collaborative teamwork. The project allows users to create, organize, and track tasks, serving as an analogous platform to Trello for team-based task management.
## Getting Started

To get started with TeamManagementApp, simply follow these steps:

1. Clone this repository.
2. Install Docker on your system if not already installed.
3. Run the application using Docker:

   ```bash
   docker-compose up
   ```
## Usage
TeamManagementApp provides a RESTful API for creating and managing boards, tables, and cards. You can interact with the API using HTTP requests or Swagger.
## Architecture
TeamManagementApp follows the principles of Clean Architecture and is divided into four main parts:
1. **Application**: Contains the application's business logic, including command and query handlers.
2. **Domain**: Defines the core domain model and business rules of the application.
3. **Infrastructure**: Provides implementations for data access, including the use of Entity Framework for working with PostgreSQL.
4. **WebApi**: Implements the RESTful API endpoints and handles incoming HTTP requests.
## Technologies Used
* C#
* ASP.NET Core
* Entity Framework
* Postgresql
* NUnit
* FluentAssertions
* FluentValidation
* Swagger(API Documentation)
* Bearer JWT Authentication
* Docker
