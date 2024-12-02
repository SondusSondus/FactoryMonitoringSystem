# FactoryMonitoringSystem

**FactoryMonitoringSystem** is a comprehensive, feature-rich backend application built using .NET Core 8. It is designed for managing and monitoring industrial environments efficiently, supporting factories, machines, sensors, and real-time notifications. The system adopts modern architectural patterns like Domain-Driven Design (DDD), CQRS, and Chain of Responsibility while incorporating advanced features such as robust authentication, custom caching, and dynamic notifications to ensure scalability, security, and maintainability.

---

## Key Features and Architecture

### **1. Modern Architectural Design**
- **Domain-Driven Design (DDD)**:
  - Modular structure with clear separation of concerns across layers:
    - **Application**: Handles command and query logic.
    - **Domain**: Encapsulates core business rules.
    - **Persistence**: Manages database interactions.
    - **Shared**: Provides reusable components and utilities.
    - **Infrastructure**: Handles external services, such as notifications.
  - Uses assembly markers (`IApplicationAssemblyMarker`, `IDomainAssemblyMarker`, etc.) for organizing dependencies.
- **CQRS Pattern**:
  - Segregates read and write operations for scalability and performance.
  - Implements **ReadDbContext** and **WriteDbContext** to handle queries and commands independently.
- **ErrorOr Pattern**:
  - Centralized error handling ensures consistent error responses across the system.
- **Chain of Responsibility Pattern**:
  - Enables flexible and modular request handling using middleware layers.

---

### **2. Dependency Injection**
- Uses **Autofac** for dynamic dependency injection and assembly scanning:
  - Registers services from all layers dynamically based on lifetimes:
    - **Scoped**, **Transient**, and **Singleton** dependencies.
  - Code example:
    ```csharp
    var applicationAssembly = typeof(IApplicationAssemblyMarker).Assembly;
    var domainAssembly = typeof(IDomainAssemblyMarker).Assembly;

    containerBuilder.RegisterAssemblyTypes(applicationAssembly, domainAssembly)
        .AssignableTo<IScopedDependency>()
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();

    containerBuilder.RegisterAssemblyTypes(applicationAssembly, domainAssembly)
        .AssignableTo<ITransientDependency>()
        .AsImplementedInterfaces()
        .InstancePerDependency();
    ```
- **Mapster IMapper** for lightweight object mapping:
    ```csharp
    containerBuilder.RegisterType<MapsterMapper.Mapper>()
        .As<MapsterMapper.IMapper>()
        .InstancePerLifetimeScope();
    ```

---

### **3. Application Service**
- A reusable and generic base service for application-level logic:
  - Provides convenient access to key components such as:
    - `IMediator`
    - `ILogger`
    - `ICacheService`
    - `WriteRepository`
    - `ReadRepository`
    - `CurrentUser`
  - Automatically generates unique GUIDs for operations.
  - Simplifies context-aware dependency resolution.

---

### **4. Generic Read and Write Repositories**
- Implements **IReadRepository** and **IWriteRepository** to abstract database interactions:
  - **Read Repository**:
    - Optimized for querying data efficiently.
    - Supports **No-Tracking** operations for performance.
  - **Write Repository**:
    - Handles data manipulation operations such as `Add`, `Update`, and `Delete`.
    - Ensures separation of concerns by isolating query and command logic.

---
### **5. Authentication and Authorization**
- **JWT-based authentication** with secure token handling using **HttpOnly Cookies**.
- **AuthController**:
  - Provides endpoints for **Login**, **Logout**, and **RefreshToken**.
  - Secures user credentials and handles token refresh for persistent sessions.
- **AccountController** manages:
  - **Registration**, **Email Verification**, **Forgot Password**, and **Confirm Password** workflows.
- **Fluent and Asynchronous Login Flow**:
  - Uses **AuthenticateAsync** with custom **FluentExtensions** to perform step-by-step validation:
    - Validates user existence, email verification, and lockout status.
    - Checks password validity and tracks failed login attempts.
    - Handles successful login by generating access and refresh tokens.
- **FluentExtensions** provides reusable methods for:
  - Asynchronous validation with custom error handling.
  - Transformation of `Task` and `ErrorOr` objects.
  - Example:
    ```csharp
    public static async Task<ErrorOr<T>> Validate<T>(
        this Task<ErrorOr<T>> task,
        Func<T, bool> predicate,
        Func<ErrorOr<T>> errorFactory)
    {
        var result = await task;
        return predicate(result.Value) ? result : errorFactory();
    }
    ```

---

### **6. User and Role Management**
- **UserController**: Full CRUD operations for user accounts, including:
  - **GetUsers**, **GetUser by ID**, **CreateUser**, **UpdateUser**, **DeleteUser**, **UnlockedUser**, and **ResetPassword**.
- **UserProfileController** manages profile operations:
  - **GetProfile**, **UpdateUserAsync**, and **ChangePassword** for logged-in users.
- **Role-Based Access Control** (RBAC) with **Admin** and **User** roles to limit access to resources based on roles.

---

### **7. Factory, Machine, and Sensor Management**
- **FactoryController**:
  - Manages factories with endpoints to create, retrieve, update, delete, and list factories.
- **MachineController**:
  - Handles CRUD operations for machines within the factory.
- **SensorController**:
  - Provides endpoints for creating, retrieving, updating, and deleting sensors.
- **SensorMachineController**:
  - Manages relationships between sensors and machines.
  - Tracks sensor values over time for real-time monitoring.

---
### **8. Custom ApiController**
- Provides a base `ApiController` for consistent API operations:
  - Centralized dependency resolution for services like:
    - `IMediator`
    - `ILogger`
    - `IMapper`
    - `CurrentUser`
  - Simplifies error handling with custom `Problem` methods that handle various error types such as:
    - Validation errors
    - Not found
    - Unauthorized
    - Conflict

---

### **9. Custom Caching Middleware**
- Implements tailored **Caching Middleware** to manage caching efficiently:
  - Uses **In-Memory Caching** for frequently accessed data.
  - Automatically handles cache invalidation and updates.
  - Reduces database calls, improving application performance.
---
### **10. Monitoring and Notifications**
- **MonitoringTaskScheduler**:
  - Automates periodic monitoring using **Hangfire**.
  - Tasks include checking sensor-machine values and triggering notifications.
- **MachineMonitoringService**:
  - Monitors real-time sensor values and detects out-of-range data.
  - Sends critical alerts to operators/admins using **SignalR**.
- **Generic Notification System**:
  - Dynamically resolves notification strategies:
    - **EmailNotificationStrategy** for email alerts.
    - **InAppNotificationStrategy** for in-app notifications.
  - Example of the `NotificationStrategyResolver`:
    ```csharp
    public class NotificationStrategyResolver : INotificationStrategyResolver
    {
        private readonly IComponentContext _context;

        public INotificationStrategy Resolve(NotificationSystemModelEnum notificationType)
        {
            if (!_strategyMap.TryGetValue(notificationType, out var strategyName))
            {
                throw new NotSupportedException($"Notification strategy {notificationType} not supported");
            }
            return _context.ResolveNamed<INotificationStrategy>(strategyName);
        }
    }
    ```

---


### **11. Database Features**
- **Database Contexts**:
  - **ReadDbContext**: Optimized for read operations using **No-Tracking** queries.
  - **WriteDbContext**:
    - Tracks entity changes and manages audit fields (e.g., CreatedBy, UpdatedBy).
    - Handles concurrency conflicts and publishes domain events.
- **Out-of-Range Sensor Values View**:
  - **View_SensorValuesOutOfRange** retrieves data where sensor values exceed acceptable thresholds:
    ```sql
    CREATE VIEW View_SensorValuesOutOfRange AS
    SELECT 
        TSM.Value AS Value,
        S.Name AS SensorName,
        M.Name AS MachineName
    FROM 
        TrackingSensorMachineValues AS TSM
    LEFT JOIN 
        SensorMachines AS SM ON SM.Id = TSM.SensorMachineId
    LEFT JOIN 
        Sensors AS S ON S.Id = SM.SensorId
    LEFT JOIN 
        Machines AS M ON M.Id = SM.MachineId
    WHERE 
        (TSM.Value < S.MinValue OR TSM.Value > S.MaxValue)
        AND TSM.IsThresholdBreached = 0;
    ```

---

### **12. Validation and Error Handling**
- **FluentValidation**: Ensures precise input validation.
- **Behavior Validation**: Centralizes validation logic.
- **ErrorOr Pattern**: Provides consistent error management across the application.

---

### **13. Security Features**
- **CORS Policy**: Secures cross-origin requests.
- **JWT Authentication**: Manages token-based authentication.
- **HttpOnly Cookies**: Protects sensitive tokens from client-side access.

---

### **14. Logging and Monitoring**
- Uses **Serilog** for structured logging of events and activities.

---

### **15. Real-Time Communication**
Utilizes SignalR for real-time notifications and updates to user groups. Specifically, SignalR is employed to:
-Send Notifications to Users: Deliver instant notifications to users, ensuring they receive timely updates and alerts directly within the application.
-Monitor Sensor Values: Automatically notify users when sensor values exceed or fall below predefined thresholds, enabling immediate awareness and response to out-of-range conditions.

---

### **16. Swagger Integration**
- Provides interactive API documentation for easy testing and integration.

---

## Technologies Used

| Technology             | Purpose                                                   |
|-------------------------|-----------------------------------------------------------|
| **.NET Core 8**        | Backend framework for scalable applications.              |
| **SQL Server**         | Relational database for structured data.                  |
| **Entity Framework**   | ORM for data handling.                                    |
| **Autofac**            | Dependency injection with assembly scanning.              |
| **SignalR**            | Real-time communication.                                  |
| **Hangfire**           | Background job scheduling and processing.                 |
| **FluentValidation**   | Input validation and business rule enforcement.           |
| **Mapster**            | Lightweight object mapping for DTOs and entities.         |
| **Serilog**            | Structured and detailed logging.                          |
| **Swagger/OpenAPI**    | Interactive API documentation for easy testing.           |

---

## How It Works

### Authentication and Authorization
- Manages user sessions with **JWT Tokens** stored securely in **HttpOnly Cookies**.
- Validates login credentials, tracks login attempts, and handles lockout functionality.
- Allows for user registration, email verification, password reset, and secure logout.

### User, Role, and Profile Management
- Supports CRUD operations for user accounts and roles.
- Provides APIs for viewing and updating user profiles and managing passwords.
- Implements RBAC to control access to specific functionalities.

### Factory, Machine, and Sensor Management
- Full CRUD capabilities for factories, machines, and sensors.
- Allows associations between sensors and machines and enables real-time tracking of sensor values.

### Monitoring and Notifications
- Periodic monitoring of sensor data using **Hangfire** for scheduled tasks.
- Detects out-of-range values and sends critical alerts via **SignalR**.
- Employs a generic notification system with strategies for email and in-app alerts.

### Performance Optimization
- **Custom Caching Middleware** caches frequently accessed data to reduce load on the database.
- Utilizes **ReadDbContext** and **WriteDbContext** with appropriate configurations for optimal data handling.

### Validation and Logging
- Uses **FluentValidation** for declarative validation of requests.
- **Serilog** provides structured logging for tracing operations, errors, and important events.

---

## Setup Instructions

1. **Clone the Repository**:
   ```bash
   git clone  https://github.com/SondusSondus/FactoryMonitoringSystem.git
   ```
2. **Navigate to the Project Directory**:
   ```bash
   cd FactoryMonitoringSystem
   ```
3. **Restore Dependencies**:
   ```bash
   dotnet restore
   ```
4. **Configure Settings**:
   - Update the `appsettings.json` file with your database connection string, JWT settings,EmailSettings and other required configurations.
   - Check `UserConfiguration` file and change email to your email and use password ***Admin@123***
    ```csharp
        builder.HasData(
                   new User
                   {
                       Id = Guid.NewGuid(),
                       Username = "Admin",
                       Email = "youremail",
                       RoleId = (int)RolesEnum.Admin,
                       IsEmailVerified = true,
                       PasswordHash = "$2a$11$tY9GFaoDWF8iw.NyaHu3x.e9iBaeUWjGb9pW7N5DFtmKDO6HmTB3C" 
  
                   });
      ```
    
5. **Run the Application**:
   ```bash
   dotnet run
   ```
6. **Access the API Documentation**:
   - Visit `http://localhost:<port>/swagger` to view and test the API endpoints using Swagger.

---
