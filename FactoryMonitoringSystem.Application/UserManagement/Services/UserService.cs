﻿using ErrorOr;
using FactoryMonitoringSystem.Application.Common.EventHandler;
using FactoryMonitoringSystem.Application.Contracts.Common.Services;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Models.Requests;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Services;
using FactoryMonitoringSystem.Application.UserManagement.Events.SendVerificationEmail;
using FactoryMonitoringSystem.Domain.UsersManagement.Entities;
using FactoryMonitoringSystem.Shared;
using FactoryMonitoringSystem.Shared.Utilities.Enums;
using FactoryMonitoringSystem.Shared.Utilities.GeneralModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading;
using static FactoryMonitoringSystem.Shared.Utilities.Constant.Errors;
namespace FactoryMonitoringSystem.Application.UserManagement.Services
{
    public class UserService : ApplicationService<UserService, User>, IUserService, IScopedDependency
    {

        public UserService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        public async Task<ErrorOr<Success>> RegisterUserAsync(SingUpRequest singUpRequest, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Register user with information {email},{Nmae}", singUpRequest.Email,singUpRequest.Username);
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
                RoleId= (int)RolesEnum.User,
                IsEmailVerified = false
            };
            WriteRepository.Add(user);
            await WriteRepository.SaveChangesAsync(cancellationToken);
            Logger.LogInformation("Create user with  email {Email} in database", user.Email);
            Logger.LogInformation("Create user successfully.");
            await SendVerificationEmail(singUpRequest.Email, verificationCode, cancellationToken);

            return Result.Success;
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
                                <p>Please use this code to complete your registration.</p>
                            </body>
                        </html>";

            Logger.LogInformation($"Send email to user {toEmail} with subject {subject} and body {body}");
            //await _emailService.SendEmailAsync(new EmailModel(toEmail, subject, body), cancellationToken);
            var conflictEvent = new SendVerificationEmailEvent(new EmailModel(toEmail, subject, body));
            await Mediator.Publish(conflictEvent);
        }

        public async Task UpdateRefreshToken(string refreshToken, CancellationToken cancellationToken)
        {
            Logger.LogInformation($"Updated refresh token: {refreshToken} for user {CurrentUser.Email}");
            var user = await ReadRepository.FindAsync(user => user.Id == LoggedInUserId, cancellationToken);
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow;
            await UpdateUser(user,cancellationToken);
            Logger.LogInformation("Update refresh token successfully.");

        }



        public async Task<ErrorOr<Success>> UpdateUserProfile(UpdateUserRequest updateUser, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Update user profile for user {Email} {UserId}", CurrentUser.Email, LoggedInUserId);
            var user = await ReadRepository.FindAsync(user => user.Id == LoggedInUserId, cancellationToken);
            user.Username = updateUser.UserName;
            user.Email = updateUser.Email;
            await UpdateUser(user, cancellationToken);
            Logger.LogInformation("Update user profile successfully.");
            return Result.Success;
        }

        private async Task UpdateUser(User user, CancellationToken cancellationToken)
        {
            WriteRepository.Update(user);
            await WriteRepository.SaveChangesAsync(cancellationToken);
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

        private string GenerateVerificationCode()
        {
            return new Random().Next(100000, 999999).ToString(); // Generates a 6-digit code
        }

        public async Task<ErrorOr<UserResponse>> GetUserAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation($"Retrieve user {CurrentUser.Email}");
            var resp = await ReadRepository.GetByIdAsync(LoggedInUserId, cancellationToken);
            return Mapper.Map<UserResponse>(resp);
        }

        // Change Password
        public async Task<ErrorOr<Success>> ChangePasswordAsync(ChangePasswordRequest changePassword, CancellationToken cancellationToken)
        {
            Logger.LogInformation($"Change password for user {CurrentUser.Email}");
            var user = await ReadRepository.GetByIdAsync(LoggedInUserId, cancellationToken);
            if (user == null)
            {
                Logger.LogInformation(UserError.UserNotFound.Description);
                return UserError.UserNotFound;
            }

            // Verify current password
            if (BCrypt.Net.BCrypt.Verify(changePassword.CurrentPassword, user.PasswordHash))
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
    }
}
