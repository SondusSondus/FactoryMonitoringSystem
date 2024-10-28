using ErrorOr;
using MediatR;

namespace FactoryMonitoringSystem.Application.UserManagement.Commands.DeleteUser
{
    public record DeleteUserCommand(Guid id) :IRequest<ErrorOr<Success>>;
   
}
