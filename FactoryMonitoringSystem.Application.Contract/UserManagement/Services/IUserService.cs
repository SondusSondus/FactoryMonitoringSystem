﻿using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Models.Requests;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Models.Responses;


namespace FactoryMonitoringSystem.Application.Contracts.UserManagement.Services
{
    public interface IUserService
    {
        Task<ErrorOr<Success>> UpdateUserProfile(UpdateUserRequest updateUser, CancellationToken cancellationToken);
        Task UpdateRefreshToken(string refreshToken, CancellationToken cancellationToken);
        Task<ErrorOr<Success>> RegisterUserAsync(SingUpRequest singUpRequest, CancellationToken cancellationToken);
        Task<ErrorOr<Success>> VerifyEmailAsync(VerifyEmailRequest verifyEmail, CancellationToken cancellationToken);
        Task<ErrorOr<UserResponse>> GetUserAsync(CancellationToken cancellationToken);
        Task<ErrorOr<Success>> ChangePasswordAsync(ChangePasswordRequest changePassword, CancellationToken cancellationToken);
        Task<ErrorOr<Success>> UpdateRefreshTokenToInValid(CancellationToken cancellationToken);


    }
}
