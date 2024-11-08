# FactoryMonitoringSystem

**FactoryMonitoringSystem** is a robust and scalable backend application built with .NET Core 8. It is designed to provide comprehensive monitoring and management features for factory environments. The application follows a modern architecture and integrates cutting-edge technologies and patterns to ensure high performance, maintainability, and reliability.

## Key Features

1. **Domain-Driven Design (DDD)**: Implements DDD principles to create a well-organized and modular architecture.
2. **CQRS Pattern**: Separates the application's read and write responsibilities for improved scalability and performance.
3. **Error Handling with ErrorOr**: Ensures consistent error management across the application.
4. **Real-Time Communication**: Utilizes **SignalR** for real-time data updates and notifications.
5. **Background Processing**: Leverages **Hangfire** for efficient background job scheduling and execution.
6. **Health Monitoring**: Implements **Health Checks** to monitor the application's health and readiness.
7. **Caching**: Uses **In-Memory Caching** to improve application performance and reduce database calls.
8. **Secure Communication**:  
   - **CORS Policy**: Ensures secure and controlled cross-origin requests.  
   - **JWT Authentication**: Securely manages user authentication with JSON Web Tokens.  
   - **HttpOnly Cookies**: Protects sensitive authentication data.
9. **Logging and Monitoring**: Uses **Serilog** for detailed and structured logging.
10. **Validation**:  
    - **FluentValidation**: Ensures precise and declarative input validation.  
    - **Behavior Validation**: Centralized validation logic for streamlined processing.
11. **Notification System**: Implements a custom event-driven system to send notifications based on application events.
12. **Dependency Injection**: Automatically resolves dependencies with efficient auto DI registration.
13. **Swagger Integration**: Provides an interactive API documentation for seamless testing and integration.

## Technologies Used

- **Backend Framework**: .NET Core 8
- **Database**: SQL Server
- **Real-Time Communication**: SignalR
- **Task Scheduling**: Hangfire
- **Validation**: FluentValidation, Behavioral Validation
- **Authentication**: JWT Tokens, HttpOnly Cookies
- **Caching**: In-Memory Caching
- **Logging**: Serilog
- **API Documentation**: Swagger/OpenAPI

## Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/FactoryMonitoringSystem.git
   ```
2. Navigate to the project directory:
   ```bash
   cd FactoryMonitoringSystem
   ```
3. Restore dependencies:
   ```bash
   dotnet restore
   ```
4. Update the appsettings file with your configuration.
5. Run the application:
   ```bash
   dotnet run
   ```

## Contributing

Contributions are welcome! Feel free to submit issues or pull requests to enhance the application.
