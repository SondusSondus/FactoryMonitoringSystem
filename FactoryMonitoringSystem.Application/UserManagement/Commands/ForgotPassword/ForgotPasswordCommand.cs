using ErrorOr;
using MediatR;

namespace FactoryMonitoringSystem.Application.UserManagement.Commands.ForgotPassword
{
    public record ForgotPasswordCommand(string email) : IRequest<ErrorOr<Success>>;

}
