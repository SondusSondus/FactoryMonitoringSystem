using ErrorOr;
using MediatR;

namespace FactoryMonitoringSystem.Application.UserManagement.Commands.ResetPasswordUser
{
    public record ResetPasswordUserCommand(Guid Id) : IRequest<ErrorOr<Success>>;

}
