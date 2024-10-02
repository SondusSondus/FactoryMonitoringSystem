using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Auth.Models.Requests;
using FactoryMonitoringSystem.Application.Contracts.Auth.Models.Responses;
using System.Threading;


namespace FactoryMonitoringSystem.Application.Contracts.Auth.Services
{
    public interface IAuthService
    {
        Task<ErrorOr<LoginResult>> AuthenticateAsync(LoginRequest loginRequest, CancellationToken cancellationToken);

    }
}
