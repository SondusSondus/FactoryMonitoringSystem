using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Auth.Models.Responses;
using FactoryMonitoringSystem.Domain.UsersManagement.Entities;

namespace FactoryMonitoringSystem.Application.Contracts.Auth.Services
{
    public interface ITokenGenerator
    {
        ErrorOr<AuthenticationResult> GenerateToken(User user, CancellationToken cancellationToken);
        ErrorOr<AuthenticationResult> GenerateToken(CancellationToken cancellationToken);
    }
}
