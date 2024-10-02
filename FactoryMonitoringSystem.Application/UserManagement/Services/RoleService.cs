using FactoryMonitoringSystem.Application.Contracts.UserManagement.Services;
using FactoryMonitoringSystem.Domain.Common.Repositories;
using FactoryMonitoringSystem.Domain.UsersManagement.Entities;
using FactoryMonitoringSystem.Shared;


namespace FactoryMonitoringSystem.Application.UserManagement.Services
{

    public class RoleService : IRoleService, IScopedDependency
    {
        private readonly IWriteRepository<Role> _writeRepository;
        private readonly IReadRepository<Role> _readRepository;

        public RoleService(IWriteRepository<Role> writeRepository, IReadRepository<Role> readRepository)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
        }

        public async Task<Role> CreateRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            if (await _readRepository.AnyAsync(r => r.RoleName == roleName, cancellationToken))
            {
                throw new Exception("Role already exists.");
            }

            var role = new Role { RoleName = roleName };
            _writeRepository.Add(role);
            await _writeRepository.SaveChangesAsync(cancellationToken);
            return role;
        }
    }
}
