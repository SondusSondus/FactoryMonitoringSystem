

using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Models.Requests;
using MediatR;

namespace FactoryMonitoringSystem.Application.UserManagement.Commands.ChangePassword
{
    public record ChangePasswordCommand(ChangePasswordRequest changePassword) : IRequest<ErrorOr<Success>>;
}
