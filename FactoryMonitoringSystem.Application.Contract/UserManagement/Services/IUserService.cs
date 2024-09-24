using FactoryMonitoringSystem.Domain.UsersManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Contracts.UserManagement.Services
{
    public interface IUserService
    {
        Task<User> RegisterAsync(string username, string email, string password);
        Task<User> GetUserByUsernameAsync(string username);
    }
}
