using ErrorOr;
using MediatR;

namespace FactoryMonitoringSystem.Application.UserManagement.Commands.DeleteUser
{
    public record DeleteUserCommand(Guid UserId) :IRequest<ErrorOr<Success>>;
   
}
