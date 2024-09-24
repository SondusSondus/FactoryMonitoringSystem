using FactoryMonitoringSystem.Application.Contracts.UserManagement.Services;
using FactoryMonitoringSystem.Domain.Common.Repositories;
using FactoryMonitoringSystem.Domain.UsersManagement.Entities;
using FactoryMonitoringSystem.Domain.UsersManagement.Specifications;

namespace FactoryMonitoringSystem.Application.UserManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IWriteRepository<User> _writeRepository;
        private readonly IReadRepository<User> _readRepository;

        public UserService(IWriteRepository<User> writeRepository, IReadRepository<User> readRepository)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
        }

        public async Task<User> RegisterAsync(string username, string email, string password)
        {

            var spec = new UserWithNameEmailSpecification(username, email);
            // Check if the user already exists
            if (await _readRepository.AnyAsync(spec))
            {
                throw new Exception("User already exists.");
            }

            // Hash the password
             var passwordHash = BCrypt.Net.HashPassword(password);

            // Create the new user
            var user = new User
            {
                Username = username,
                Email = email,
               // PasswordHash = passwordHash,
                Role = "User", // Default role
            };

            // Save the user to the database
            _writeRepository.Add(user);
            await _writeRepository.SaveChangesAsync();

            return user;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return (User)await _readRepository.FindAsync(u => u.Username == username);
        }
    }
}
