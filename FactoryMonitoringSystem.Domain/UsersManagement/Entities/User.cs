using FactoryMonitoringSystem.Domain.Common.Entities;


namespace FactoryMonitoringSystem.Domain.UsersManagement.Entities
{
    public class User : BaseEntity<Guid>
    {
        public string Username { get; set; } // Username for login
        public string Email { get; set; } // Email for communication and authentication
        public string PasswordHash { get; set; } // Hashed password (we don't store plaintext passwords)
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();  // User's roles
        public int FailedLoginAttempts { get; set; } = 0; // Track failed login attempts
        public DateTime? LockoutEnd { get; set; } // Account lockout time after too many failed attempts
        public bool IsEmailVerified { get; set; } = false; // New property to track email verification status
        public string EmailVerificationCode { get; set; } // New property to store the verification code
        public DateTime? EmailVerificationCodeExpiration { get; set; } // Expiry for the verification code



    }
}
