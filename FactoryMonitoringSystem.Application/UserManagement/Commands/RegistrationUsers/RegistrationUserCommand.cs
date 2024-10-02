using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Models.Requests;
using MediatR;

namespace FactoryMonitoringSystem.Application.UserManagement.Commands.RegistrationUsers
{
    public record RegistrationUserCommand(SingUpRequest SingUpRequest) : IRequest<ErrorOr<Success>>;

}
