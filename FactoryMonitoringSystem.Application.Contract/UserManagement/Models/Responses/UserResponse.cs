


namespace FactoryMonitoringSystem.Application.Contracts.UserManagement.Models.Responses
{
    public record UserResponse
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public RoleResponse Role { get; set; }
    }
}
