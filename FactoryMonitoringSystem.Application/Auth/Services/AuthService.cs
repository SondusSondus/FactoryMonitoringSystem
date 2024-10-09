using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Auth.Models.Requests;
using FactoryMonitoringSystem.Application.Contracts.Auth.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.Auth.Services;
using FactoryMonitoringSystem.Domain.UsersManagement.Entities;
using FactoryMonitoringSystem.Shared;
using FactoryMonitoringSystem.Shared.Utilities.GeneralModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static FactoryMonitoringSystem.Shared.Utilities.Constant.Errors;
using static FactoryMonitoringSystem.Shared.Utilities.GeneralServices.FluentExtensions;
namespace FactoryMonitoringSystem.Application.Auth.Services
{

    public class AuthService : ApplicationService<AuthService, User>, IAuthService, IScopedDependency
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly AppOptions _appOptions;

        public AuthService(ITokenGenerator tokenGenerator, IOptions<AppOptions> appOptions, IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
            _tokenGenerator = tokenGenerator;
            _appOptions = appOptions.Value;
        }

        // Asynchronous and Fluent Authenticate method
        public async Task<ErrorOr<LoginResult>> AuthenticateAsync(LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            Logger.LogInformation("The user {Email} try to login", loginRequest.Email);
            // Step 1: Get the user and handle errors if the user is not found
            var userResult = await GetUserAsync(loginRequest.Email, cancellationToken);
            if (userResult.IsError)
            {
                Logger.LogError(AuthError.InvalidCredentials.Description);
                return userResult.Errors;
            }

            var user = userResult.Value; // Unwrap the user from ErrorOr<User>

            // Step 2: Validate each condition in the authentication flow
            var map = await Task.FromResult(user)
                .Validate(user => user != null, () => HandleInvalidCredentials(loginRequest.Email))
                .Validate(user => user.IsEmailVerified, () => HandleEmailNotVerified(loginRequest.Email))
                .Validate(user => !IsLockedOut(user), () => HandleAccountLockout(user))
                .Validate(user => ValidatePassword(loginRequest.Password, user.PasswordHash), () => HandleFailedLoginAttemptAsync(user, cancellationToken))
                .Map(user => HandleSuccessfulLoginAsync(user, cancellationToken));
            if (map.IsError)
                return map.Errors;
            return map.Value;

        }

        // Get user by username or email
        private async Task<ErrorOr<User>> GetUserAsync(string email, CancellationToken cancellationToken)
        {
            var user = await ReadRepository.FindAsyncInclude(
                cancellationToken,
                user => user.Username == email || user.Email == email,
                user => user.Role  // Include UserRoles and ThenInclude Role
            );

            return user == null ? AuthError.InvalidCredentials : user;
        }

        // Check if user is locked out
        private static bool IsLockedOut(User user) =>
            user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTime.UtcNow;

        // Validate password
        private static bool ValidatePassword(string inputPassword, string storedPasswordHash) =>
            BCrypt.Net.BCrypt.Verify(inputPassword, storedPasswordHash);

        // Handle failed login attempts (increment and lockout logic)
        private async Task<ErrorOr<User>> HandleFailedLoginAttemptAsync(User user, CancellationToken cancellationToken)
        {
            user.FailedLoginAttempts++;

            if (user.FailedLoginAttempts >= _appOptions.MaxFailedAttempts)
            {
                user.LockoutEnd = DateTime.UtcNow.AddMinutes(_appOptions.LockoutDurationMinutes);
                await UpdateUser(user, cancellationToken);
                Logger.LogError(AuthError.AccountLockedOut(user.LockoutEnd.Value).Description);
                return AuthError.AccountLockedOut(user.LockoutEnd.Value);
            }
            await UpdateUser(user, cancellationToken);
            Logger.LogError(AuthError.InvalidCredentials.Description);
            return AuthError.InvalidCredentials;
        }

        private async Task UpdateUser(User user, CancellationToken cancellationToken)
        {
            WriteRepository.Update(user);
            await WriteRepository.SaveChangesAsync(cancellationToken);
        }
        // Handle invalid credentials
        private ErrorOr<User> HandleInvalidCredentials(string email)
        {
            Logger.LogError("Invalid credentials for {Email} or password", email);
            return AuthError.InvalidCredentials;
        }

        // Handle email not verified
        private ErrorOr<User> HandleEmailNotVerified(string email)
        {
            Logger.LogError(AuthError.EmailNotVerified.Description, email);
            return AuthError.EmailNotVerified;
        }

        // Handle account lockout (log and return lockout error)
        private ErrorOr<User> HandleAccountLockout(User user)
        {
            Logger.LogError(AuthError.AccountLockedOut(user.LockoutEnd!.Value).Description);
            return AuthError.AccountLockedOut(user.LockoutEnd!.Value);
        }

        // Handle successful login (reset attempts and lockout)
        private async Task<ErrorOr<LoginResult>> HandleSuccessfulLoginAsync(User user, CancellationToken cancellationToken)
        {
            var tokenResult = _tokenGenerator.GenerateToken(user, cancellationToken);
            if (tokenResult.IsError)
            {
                Logger.LogError("Error generating token for {Email}", user.Email);
                Logger.LogError(General.TokenGeneratorFailure.Description);
                return tokenResult.Errors;
            }

            // Reset failed login attempts after successful login
            user.FailedLoginAttempts = 0;
            user.LockoutEnd = null;
            user.RefreshToken = tokenResult.Value.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_appOptions.RefreshTokenExpirationDays);
            await UpdateUser(user, cancellationToken);

            var loginResponse = Mapper.Map<LoginResponse>(user);
            return new LoginResult(loginResponse, new AuthenticationResult(tokenResult.Value.AccessToken, tokenResult.Value.RefreshToken));
        }
    }
}
