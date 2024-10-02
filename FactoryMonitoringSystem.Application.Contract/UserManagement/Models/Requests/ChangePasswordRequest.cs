

namespace FactoryMonitoringSystem.Application.Contracts.UserManagement.Models.Requests
{
    public record ChangePasswordRequest(string CurrentPassword, string NewPassword, string ConfirmPassword);
   
}
