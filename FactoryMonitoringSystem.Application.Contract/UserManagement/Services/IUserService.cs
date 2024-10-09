using ErrorOr;
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
        Task<ErrorOr<Success>> ForgotPasswordByEmail(string email, CancellationToken cancellationToken);
        Task<ErrorOr<Success>> ConfirmPassword(ConfirmPasswordRequest confirmPassword, CancellationToken cancellationToken);
        Task<ErrorOr<List<UserResponse>>> GetUsers(CancellationToken cancellationToken);
        Task<ErrorOr<UserResponse>> GetUserById(Guid Id, CancellationToken cancellationToken);
        Task<ErrorOr<Success>> UnlockedUser(Guid Id, CancellationToken cancellationToken);
        Task<ErrorOr<Success>> ResetPasswordUser(Guid Id, CancellationToken cancellationToken);

    }
}
