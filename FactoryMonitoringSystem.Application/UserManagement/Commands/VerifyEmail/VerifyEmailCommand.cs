using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Models.Requests;
using MediatR;

namespace FactoryMonitoringSystem.Application.UserManagement.Commands.VerifyEmail
{
    public record VerifyEmailCommand(VerifyEmailRequest VerifyEmailRequest) : IRequest<ErrorOr<Success>>;

}
