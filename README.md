# FactoryMonitoringSystem

**FactoryMonitoringSystem** is a scalable backend application built with .NET Core 8, designed for monitoring and managing factory environments. It integrates advanced architectural patterns, robust authentication, real-time communication, and comprehensive management systems for users, factories, machines, sensors, and more.

---

## Key Features

### 1. **Architecture and Patterns**
- **Domain-Driven Design (DDD)**: Implements a modular and well-organized structure, separating concerns across layers.
- **CQRS Pattern**: Separates read and write operations for scalability and maintainability.
- **Chain of Responsibility Pattern**: Used for flexible request handling.
- **ErrorOr Pattern**: Ensures consistent error handling across the application.
---
### 2. Dependency Injection with Autofac**
- Leverages **Autofac** for efficient dependency injection across assemblies.
- Registers services dynamically based on the following lifetime scopes:
  - **Scoped**, **Transient**, and **Singleton**.
- Assemblies scanned include:
  - `ApplicationAssembly`, `DomainAssembly`, `PersistenceAssembly`, `SharedAssembly`, and `InfrastructureAssembly`.
- Example setup:
  ```csharp
  containerBuilder.RegisterAssemblyTypes(applicationAssembly, domainAssembly, persistenceAssembly, sharedAssembly, infrastructureAssembly)
      .AssignableTo<IScopedDependency>()
      .AsImplementedInterfaces()
      .InstancePerLifetimeScope();
  ```
- **Mapster Mapper** is used for lightweight object mapping:
  ```csharp
  containerBuilder.RegisterType<MapsterMapper.Mapper>()
      .As<MapsterMapper.IMapper>()
      .InstancePerLifetimeScope();
  ```

---

### 2. **Application Service**
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

### 3. **Generic Read and Write Repositories**
- Implements **IReadRepository** and **IWriteRepository** to abstract database interactions:
  - **Read Repository**:
    - Optimized for querying data efficiently.
    - Supports **No-Tracking** operations for performance.
  - **Write Repository**:
    - Handles data manipulation operations such as `Add`, `Update`, and `Delete`.
    - Ensures separation of concerns by isolating query and command logic.

---
### **3. Authentication and Authorization**
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

### **4. User and Role Management**
- **UserController**: Full CRUD operations for user accounts, including:
  - **GetUsers**, **GetUser by ID**, **CreateUser**, **UpdateUser**, **DeleteUser**, **UnlockedUser**, and **ResetPassword**.
- **UserProfileController** manages profile operations:
  - **GetProfile**, **UpdateUserAsync**, and **ChangePassword** for logged-in users.
- **Role-Based Access Control** (RBAC) with **Admin** and **User** roles to limit access to resources based on roles.

---

### **5. Factory, Machine, and Sensor Management**
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
### 4. **Custom ApiController**
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

### 5. **Custom Caching Middleware**
- Implements tailored **Caching Middleware** to manage caching efficiently:
  - Uses **In-Memory Caching** for frequently accessed data.
  - Automatically handles cache invalidation and updates.
  - Reduces database calls, improving application performance.

---

### 6. **Authentication System**
- **AuthController**:
  - **Login**: Authenticates users and issues JWT access and refresh tokens stored as HTTP-Only cookies.
  - **Logout**: Invalidates refresh tokens and clears authentication cookies.
  - **RefreshToken**: Generates new tokens if a valid refresh token is provided.
  - Tokens are secured with:
    - **Secure** flag for HTTPS-only communication.
    - **SameSite=Strict** to prevent CSRF attacks.

---
### 13. **Monitoring and Notifications**
- **MonitoringTaskScheduler**:
  - Schedules periodic checks using **Hangfire**.
  - Executes hourly checks on sensor-machine values.
- **MachineMonitoringService**:
  - Detects out-of-range sensor values.
  - Broadcasts failure notifications to **Operators** and **Admins** via **SignalR**.
  - Logs sensor and machine details for investigation.
 
---

### 14. **Generic Notification System**:  
   - Provides a flexible and extensible notification system using strategies and events:
     - **NotificationEvent**: Represents a base record for notification events, containing the notification object and supported systems.
     - **Notification Strategies**:
       - **EmailNotificationStrategy**: Handles email-based notifications.
       - **InAppNotificationStrategy**: Handles in-app notifications.
     - **NotificationStrategyResolver**: Resolves the appropriate strategy based on the **NotificationSystemModelEnum**.
     - **NotificationEventHandler**: Processes notifications using resolved strategies and logs the status.
   - Supports the following notification systems:
     - **EmailNotification**: For sending notifications via email.
     - **InAppNotification**: For in-application notifications.
   - Registration of strategies is done using dependency injection for scalability:
     ```csharp
     containerBuilder.RegisterType<EmailNotificationStrategy>()
                     .Named<INotificationStrategy>(nameof(EmailNotificationStrategy));
     containerBuilder.RegisterType<InAppNotificationStrategy>()
                     .Named<INotificationStrategy>(nameof(InAppNotificationStrategy));
     ```
   - Example Enum for Notification Types:
     ```csharp
     public enum NotificationSystemModelEnum
     {
         EmailNotification,
         InAppNotification
     }
     ```

---

### 15. **Database Features**
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

### 15. **Validation and Error Handling**
- **FluentValidation**: Ensures precise input validation.
- **Behavior Validation**: Centralizes validation logic.
- **ErrorOr Pattern**: Provides consistent error management across the application.

---

### 16. **Security Features**
- **CORS Policy**: Secures cross-origin requests.
- **JWT Authentication**: Manages token-based authentication.
- **HttpOnly Cookies**: Protects sensitive tokens from client-side access.

---

### 17. **Logging and Monitoring**
- Uses **Serilog** for structured logging of events and activities.

---

### 18. **Real-Time Communication**
- Uses **SignalR** for real-time notifications and updates to user groups.

---

### 19. **Swagger Integration**
- Provides interactive API documentation for easy testing and integration.

---

## Technologies Used

- **Backend Framework**: .NET Core 8
- **Database**: SQL Server with Entity Framework Core
- **Real-Time Communication**: SignalR
- **Task Scheduling**: Hangfire
- **Caching**: Custom In-Memory Caching Middleware
- **Validation**: FluentValidation, Behavior Validation
- **Authentication**: JWT Tokens, HttpOnly Cookies
- **Logging**: Serilog
- **API Documentation**: Swagger/OpenAPI

