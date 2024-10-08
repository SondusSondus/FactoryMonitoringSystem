using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Models.Requests;
using MediatR;

namespace FactoryMonitoringSystem.Application.UserManagement.Commands.UpdateUser
{
    public record UpdateUserCommand(UpdateUserRequest UpdateUser) : IRequest<ErrorOr<Success>>;

}
