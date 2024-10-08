using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Models.Requests;
using MediatR; 

namespace FactoryMonitoringSystem.Application.UserManagement.Commands.ConfirmPassword
{
    public record ConfirmPasswordCommand(ConfirmPasswordRequest confirmPassword) :IRequest<ErrorOr<Success>>;
   
}
