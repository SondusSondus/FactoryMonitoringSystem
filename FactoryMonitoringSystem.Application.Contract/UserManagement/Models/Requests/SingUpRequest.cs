

namespace FactoryMonitoringSystem.Application.Contracts.UserManagement.Models.Requests
{
    public record SingUpRequest(string Username, string Email, string Password, string ConfirmPassword);

}
