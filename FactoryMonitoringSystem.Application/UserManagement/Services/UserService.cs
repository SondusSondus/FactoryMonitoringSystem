using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Models.Requests;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Services;
using FactoryMonitoringSystem.Application.UserManagement.Events.SendVerificationEmail;
using FactoryMonitoringSystem.Domain.UsersManagement.Entities;
using FactoryMonitoringSystem.Shared;
using FactoryMonitoringSystem.Shared.Utilities.Enums;
using FactoryMonitoringSystem.Shared.Utilities.GeneralModels;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static FactoryMonitoringSystem.Shared.Utilities.Constant.Errors;
namespace FactoryMonitoringSystem.Application.UserManagement.Services
{
    internal class UserService : ApplicationService<UserService, User>, IUserService, IScopedDependency
    {
        private readonly AppOptions _appOptions;
        public UserService(IHttpContextAccessor httpContextAccessor, IOptions<AppOptions> options) : base(httpContextAccessor)
        {
            _appOptions = options.Value;
        }

        public async Task<ErrorOr<Success>> RegisterUserAsync(SingUpRequest singUpRequest, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Register user with information {email},{Nmae}", singUpRequest.Email, singUpRequest.Username);
            //  Check if the username or email is already taken
            if (await ReadRepository.AnyAsync(u => u.Username == singUpRequest.Username, cancellationToken))
            {
                Logger.LogError(UserError.UsernameAlreadyExists.Description, singUpRequest.Username);
                return UserError.UsernameAlreadyExists;
            }

            if (await ReadRepository.AnyAsync(u => u.Email == singUpRequest.Email, cancellationToken))
            {
                Logger.LogError(UserError.EmailAlreadyExists.Description, singUpRequest.Email);
                return UserError.EmailAlreadyExists;
            }

            //  Hash the password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(singUpRequest.Password);

            var verificationCode = GenerateVerificationCode();

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = singUpRequest.Username,
                Email = singUpRequest.Email,
                PasswordHash = hashedPassword,
                EmailVerificationCode = verificationCode,
                EmailVerificationCodeExpiration = DateTime.UtcNow.AddHours(1), // Expiration time for the code
                RoleId = (int)RolesEnum.User,
                IsEmailVerified = false
            };
            WriteRepository.Add(user);
            await WriteRepository.SaveChangesAsync(cancellationToken);
            Logger.LogInformation("Create user with  email {Email} in database", user.Email);
            Logger.LogInformation("Create user successfully.");
            await SendVerificationEmail(singUpRequest.Email, verificationCode, cancellationToken);

            return Result.Success;
        }



        public async Task UpdateRefreshToken(string refreshToken, CancellationToken cancellationToken)
        {
            Logger.LogInformation($"Updated refresh token: {refreshToken} for user {CurrentUser.Email}");
            var user = await ReadRepository.FindAsync(user => user.Id == LoggedInUserId, cancellationToken);
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_appOptions.RefreshTokenExpirationDays);
            await UpdateUser(user, cancellationToken);
            Logger.LogInformation("Update refresh token successfully.");

        }


        public async Task<ErrorOr<Success>> UpdateUserProfile(UpdateUserRequest updateUser, CancellationToken cancellationToken)
        {

            var user = await ReadRepository.FindAsync(user => user.Id == LoggedInUserId, cancellationToken);
            if (user == null)
            {
                Logger.LogError(UserError.UserNotFound.Description);
                return UserError.UserNotFound;
            }
            Logger.LogInformation("Update user profile for user {Email} {UserId}", CurrentUser.Email, LoggedInUserId);
            user.Username = updateUser.Username;
            user.Email = updateUser.Email;
            await UpdateUser(user, cancellationToken);
            Logger.LogInformation("Update user profile successfully.");
            return Result.Success;



        }

        private async Task UpdateUser(User user, CancellationToken cancellationToken)
        {
            WriteRepository.Update(user);
            await WriteRepository.SaveChangesAsync(cancellationToken);
            Logger.LogInformation("Update user successfully.");

        }
        public async Task<ErrorOr<Success>> VerifyEmailAsync(VerifyEmailRequest verifyEmail, CancellationToken cancellationToken)
        {

            Logger.LogInformation($"Verify email: {verifyEmail}");
            // Find the user by email
            var user = await ReadRepository.FindAsync(u => u.Email == verifyEmail.Email, cancellationToken);
            if (user == null)
            {
                Logger.LogError(UserError.UserNotFound.Description, verifyEmail.Email);
                return UserError.UserNotFound;
            }

            // Check if the code is correct and not verifyEmail
            if (user.EmailVerificationCode != verifyEmail.VerificationCode || user.EmailVerificationCodeExpiration < DateTime.UtcNow)
            {
                Logger.LogError(UserError.InvalidOrExpiredVerificationCode.Description, verifyEmail.VerificationCode, user.EmailVerificationCodeExpiration);
                return UserError.InvalidOrExpiredVerificationCode;
            }

            // Mark the email as verified
            user.IsEmailVerified = true;
            user.EmailVerificationCode = null; // Remove the code once verified
            user.EmailVerificationCodeExpiration = null;
            await UpdateUser(user, cancellationToken);
            Logger.LogInformation("Verify email:  for user {Email} Success", verifyEmail.Email);
            Logger.LogInformation("Email verified successfully.");
            return Result.Success;
        }



        public async Task<ErrorOr<UserResponse>> GetUserAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation($"Retrieve user {CurrentUser.Email}");
            try
            {
                var result = await ReadRepository.FindAsync(
                                   user => user.Id == LoggedInUserId,
                                   cancellationToken,
                                   user => user.Role);
                Logger.LogInformation("Return User User successfully.");
                return result.Adapt<UserResponse>();
            }
            catch (Exception ex)
            {
                Logger.LogInformation(General.Unexpected.Description);
                return General.Unexpected;
            }

        }

        public async Task<ErrorOr<Success>> ChangePasswordAsync(ChangePasswordRequest changePassword, CancellationToken cancellationToken)
        {
            var user = await ReadRepository.GetByIdAsync(LoggedInUserId, cancellationToken);
            if (user == null)
            {
                Logger.LogInformation(UserError.UserNotFound.Description);
                return UserError.UserNotFound;
            }
            Logger.LogInformation($"Change password for user {CurrentUser.Email}");
            // Verify current password
            if (!BCrypt.Net.BCrypt.Verify(changePassword.CurrentPassword, user.PasswordHash))
            {
                Logger.LogInformation(UserError.PasswordNotMatch.Description);
                return UserError.PasswordNotMatch;
            }

            // Hash and update the new password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(changePassword.NewPassword);
            await UpdateUser(user, cancellationToken);
            Logger.LogInformation("Change password successfully.");

            return Result.Success;
        }

        public async Task<ErrorOr<Success>> UpdateRefreshTokenToInValid(CancellationToken cancellationToken)
        {

            var user = await ReadRepository.FindAsync(user => user.Id == LoggedInUserId, cancellationToken);
            if (user == null)
            {
                Logger.LogInformation(UserError.UserNotFound.Description);
                return UserError.UserNotFound;
            }
            Logger.LogInformation("Update user profile for user {Email} {UserId}", CurrentUser.Email, LoggedInUserId);
            user.RefreshToken = string.Empty;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(-1);
            await UpdateUser(user, cancellationToken);

            return Result.Success;
        }

        public async Task<ErrorOr<Success>> ForgotPasswordByEmail(string email, CancellationToken cancellationToken)
        {
            var user = await ReadRepository.FindAsync(user => user.Email == email, cancellationToken);
            if (user == null)
            {
                Logger.LogInformation(UserError.UserNotFound.Description);
                return UserError.UserNotFound;
            }
            Logger.LogInformation("Forgot password for user {Email}", email);
            var verificationCode = GenerateVerificationCode();
            user.IsEmailVerified = false;
            user.EmailVerificationCode = verificationCode;
            user.EmailVerificationCodeExpiration = DateTime.UtcNow.AddHours(1);
            await UpdateUser(user, cancellationToken);
            await SendVerificationEmail(email, verificationCode, cancellationToken);
            Logger.LogInformation("Forgot password successfully.");
            return Result.Success;
        }

        private string GenerateVerificationCode()
        {
            return new Random().Next(100000, 999999).ToString(); // Generates a 6-digit code
        }
        private async Task SendVerificationEmail(string toEmail, string verificationCode, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Send verification code {VerificationCode} to user {Email} ", verificationCode, toEmail);
            var subject = "Your Verification Code";
            var body = $@"
                        <html>
                            <body>
                                <p>Hello,</p>
                                <p>Your verification code is: <strong>{verificationCode}</strong></p>
                                <p>Please use this code to complete your process.</p>
                            </body>
                        </html>";

            Logger.LogInformation($"Send email to user {toEmail} with subject {subject} and body {body}");
            //await _emailService.SendEmailAsync(new EmailModel(toEmail, subject, body), cancellationToken);
            var conflictEvent = new SendVerificationEmailEvent(new EmailModel(toEmail, subject, body));
            await Mediator.Publish(conflictEvent);
        }

        public async Task<ErrorOr<Success>> ConfirmPassword(ConfirmPasswordRequest confirmPassword, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Confirm password for user {Email}", confirmPassword.Email);
            var user = await ReadRepository.FindAsync(user => user.Email == confirmPassword.Email, cancellationToken);
            if (user == null)
            {
                Logger.LogInformation(UserError.UserNotFound.Description);
                return UserError.UserNotFound;
            }
            // Check if the code is correct and not verifyEmail
            if (user.EmailVerificationCode != confirmPassword.VerificationCode || user.EmailVerificationCodeExpiration < DateTime.UtcNow)
            {
                Logger.LogError(UserError.InvalidOrExpiredVerificationCode.Description, confirmPassword.VerificationCode, user.EmailVerificationCodeExpiration);
                return UserError.InvalidOrExpiredVerificationCode;
            }

            // Mark the email as verified
            user.IsEmailVerified = true;
            user.EmailVerificationCode = null; // Remove the code once verified
            user.EmailVerificationCodeExpiration = null;
            // Hash and update the new password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(confirmPassword.NewPassword);
            await UpdateUser(user, cancellationToken);
            Logger.LogInformation("Verify password:  for user {Email} Success", confirmPassword.Email);
            Logger.LogInformation("Confirm password successfully.");
            return Result.Success;
        }

        public async Task<ErrorOr<List<UserResponse>>> GetUsers(CancellationToken cancellationToken)
        {
            try
            {
                Logger.LogInformation("Retrieve All Users");
                var users = await ReadRepository.GetAllAsync(cancellationToken, null ,user => user.Role);
                Logger.LogInformation("Retrieve Users successfully.");
                return users.Adapt<List<UserResponse>>();
            }
            catch (Exception ex) {
                Logger.LogInformation(General.Unexpected.Description);
                return General.Unexpected;
            }
           
        }

        public async Task<ErrorOr<UserResponse>> GetUserById(Guid Id, CancellationToken cancellationToken)
        {
            var user = await ReadRepository.FindAsync(user => user.Id == Id, cancellationToken, user => user.Role);
            if (user is null)
            {
                Logger.LogInformation(UserError.UserNotFound.Description);
                return UserError.UserNotFound;
            }
            Logger.LogInformation("Retrieve user  {email}", user.Email);
            Logger.LogInformation("Retrieve  User successfully");
            return user.Adapt<UserResponse>();
        }

        public async Task<ErrorOr<Success>> UnlockedUser(Guid Id, CancellationToken cancellationToken)
        {
            var user = await ReadRepository.GetByIdAsync(Id,cancellationToken);
            if (user is null)
            {
                Logger.LogInformation(UserError.UserNotFound.Description);
                return UserError.UserNotFound;
            }
            Logger.LogInformation("Unlocked user {email}", user.Email);
            user.LockoutEnd =null;
            user.FailedLoginAttempts = 0;
            await UpdateUser(user,cancellationToken);
            Logger.LogInformation("Unlocke user successfully");
            return Result.Success;
        }

        public async Task<ErrorOr<Success>> ResetPasswordUser(Guid Id, CancellationToken cancellationToken)
        {
            var user = await ReadRepository.GetByIdAsync(Id, cancellationToken);
            if (user is null)
            {
                Logger.LogInformation(UserError.UserNotFound.Description);
                return UserError.UserNotFound;
            }
            Logger.LogInformation("Reset password user {email}", user.Email);
            var verificationCode = GenerateVerificationCode();
            user.IsEmailVerified = false;
            user.EmailVerificationCode = verificationCode;
            user.EmailVerificationCodeExpiration = DateTime.UtcNow.AddHours(1);
            await UpdateUser(user, cancellationToken);
            await SendVerificationEmail(user.Email, verificationCode, cancellationToken);
            Logger.LogInformation("Reset password user successfully");
            return Result.Success;
        }

        public async Task<ErrorOr<Success>> DeleteUser(Guid Id, CancellationToken cancellationToken)
        {
            var user = await ReadRepository.GetByIdAsync(Id, cancellationToken);
            if (user is null)
            {
                Logger.LogInformation(UserError.UserNotFound.Description);
                return UserError.UserNotFound;
            }
            Logger.LogInformation("Reset password user {email}", user.Email);
            WriteRepository.Delete(user);
            await WriteRepository.SaveChangesAsync(cancellationToken);
            return Result.Success;
        }

        public async Task<ErrorOr<DateTime>> CheckUserByRefreshToken(string refreshToken, CancellationToken cancellationToken)
        {
            var user = await ReadRepository.FindAsync( user => user.RefreshToken == refreshToken, cancellationToken);
            if (user is null)
            {
                Logger.LogInformation(General.TokenRefresh.Description);
                return General.TokenRefresh;
            }
            return user.RefreshTokenExpiryTime.Value;
        }
    }
}
