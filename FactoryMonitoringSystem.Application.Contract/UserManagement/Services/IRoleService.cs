using FactoryMonitoringSystem.Domain.UsersManagement.Entities;


namespace FactoryMonitoringSystem.Application.Contracts.UserManagement.Services
{
    public interface IRoleService
    {
        Task<Role> CreateRoleAsync(string roleName, CancellationToken cancellationToken);
    }
}
