using FactoryMonitoringSystem.Application.Contracts.UserManagement.Services;
using FactoryMonitoringSystem.Domain.Common.Repositories;
using FactoryMonitoringSystem.Domain.UsersManagement.Entities;
using FactoryMonitoringSystem.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.UserManagement.Services
{
    internal class UserRoleService : IUserRoleService, IScopedDependency
    {
        private readonly IReadRepository<Role> _readRoleRepository;
        private readonly IReadRepository<UserRole> _readUserRoleRepository;
        public UserRoleService(IReadRepository<Role> readRoleRepository , IReadRepository<UserRole> readUserRoleRepository)
        {

            _readRoleRepository = readRoleRepository;
            _readUserRoleRepository = readUserRoleRepository;
        }
        public async Task<UserRole> AssignRoleToUserAsync(Guid userId, string roleName, CancellationToken cancellationToken)
        {
           

            var role = await _readRoleRepository.FindAsync(r => r.RoleName == roleName, cancellationToken);
            if (role == null) throw new Exception("Role not found.");

             return new UserRole { UserId = userId, RoleId = role.Id};
        
        }

        public Task<UserRole> GetUserWithRolesAsync(Guid userId, CancellationToken cancellationToken)
        {
            return _readUserRoleRepository.FindAsyncInclude(cancellationToken,userRole => userRole.User.Id == userId, userRole =>  userRole.Role, userRole => userRole.User);
        }
    }
}
