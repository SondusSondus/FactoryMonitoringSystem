using FactoryMonitoringSystem.Domain.UsersManagement.Entities;


namespace FactoryMonitoringSystem.Application.Contracts.UserManagement.Services
{
    public interface IUserRoleService
    {
        Task<UserRole> AssignRoleToUserAsync(Guid userId, string roleName, CancellationToken cancellationToken);
        Task<UserRole> GetUserWithRolesAsync(Guid userId, CancellationToken cancellationToken);

    }
}
