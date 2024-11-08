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

### 7. **Account Management System**
- **AccountController**:
  - **Registration**: Registers new users.
  - **VerifyEmail**: Verifies email addresses for new accounts.
  - **ForgotPassword**: Initiates the password reset process.
  - **ConfirmPassword**: Confirms and sets new passwords during the reset process.

---

### 8. **User Management System**
- **UserController**:
  - **GetUsers**: Retrieves all users.
  - **GetUser**: Fetches details of a user by ID.
  - **ResetPassword**: Resets a user's password.
  - **CreateUser**: Adds a new user to the system.
  - **UnlockedUser**: Unlocks a user's account.
  - **DeleteUser**: Deletes a user by ID.
- **UserProfileController**:
  - **GetProfile**: Fetches the profile of the currently logged-in user.
  - **UpdateUserAsync**: Updates profile details for the logged-in user.
  - **ChangePassword**: Enables users to securely change their password.

---

### 9. **Factory Management System**
- **FactoryController**:
  - **CreateFactory**: Adds new factories.
  - **UpdateFactory**: Updates factory details by ID.
  - **GetFactory**: Fetches factory details by ID.
  - **GetFactories**: Retrieves all factories.
  - **DeleteFactory**: Deletes a factory by ID.

---

### 10. **Machine Management System**
- **MachineController**:
  - **CreateMachine**: Adds new machines.
  - **UpdateMachine**: Updates machine details by ID.
  - **GetMachine**: Fetches machine details by ID.
  - **GetMachines**: Retrieves all machines.
  - **DeleteMachine**: Deletes a machine by ID.

---

### 11. **Sensor Management System**
- **SensorController**:
  - **CreateSensor**: Adds new sensors.
  - **UpdateSensor**: Updates sensor details by ID.
  - **GetSensor**: Fetches sensor details by ID.
  - **GetSensors**: Retrieves all sensors.
  - **DeleteSensor**: Deletes a sensor by ID.

---

### 12. **Sensor-Machine Management System**
- **SensorMachineController**:
  - **AddSensorToMachine**: Associates sensors with machines.
  - **GetSensorMachine**: Retrieves details of sensor-machine relationships by ID.
  - **GetAllSensorMachine**: Fetches all sensor-machine relationships.
  - **AddTrackingSensorMachineValue**: Records tracking values for sensors.
  - **GetTrackingSensorMachineValue**: Retrieves tracked values for sensor-machine combinations.

---

### 13. **Monitoring and Notifications**
- **MonitoringTaskScheduler**:
  - Schedules periodic checks using **Hangfire**.
  - Executes hourly checks on sensor-machine values.
- **MachineMonitoringService**:
  - Detects out-of-range sensor values.
  - Broadcasts failure notifications to **Operators** and **Admins** via **SignalR**.
  - Logs sensor and machine details for investigation.
- **Generic Notification System**:
  - **NotificationEvent**: Base record for notifications.
  - **Notification Strategies**:
    - **EmailNotificationStrategy**: Sends email notifications.
    - **InAppNotificationStrategy**: Handles in-app notifications.
  - **NotificationStrategyResolver**: Dynamically resolves the correct strategy based on the notification type.
  - **NotificationEventHandler**: Processes notifications using the resolved strategy.

---

### 14. **Database Features**
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

