using ErrorOr;
using MediatR;

namespace FactoryMonitoringSystem.Application.UserManagement.Commands.UnlockedUser
{
    public record UnlockedUserCommand(Guid Id) : IRequest<ErrorOr<Success>>;
   
}
