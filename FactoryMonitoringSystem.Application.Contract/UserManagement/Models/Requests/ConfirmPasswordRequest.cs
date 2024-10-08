namespace FactoryMonitoringSystem.Application.Contracts.UserManagement.Models.Requests
{
    public record ConfirmPasswordRequest(string Email, string VerificationCode, string NewPassword, string ConfirmPassword);
}
   
