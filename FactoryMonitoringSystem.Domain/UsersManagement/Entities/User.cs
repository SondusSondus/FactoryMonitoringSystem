using FactoryMonitoringSystem.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Domain.UsersManagement.Entities
{
    public class User : BaseEntity<Guid>
    {
        public string Username { get; set; } // Username for login
        public string Email { get; set; } // Email for communication and authentication
        public string PasswordHash { get; set; } // Hashed password (we don't store plaintext passwords)
        public string Role { get; set; } // User role (Admin, User, etc.)

        // Security features
        public int FailedLoginAttempts { get; set; } = 0; // Track failed login attempts
        public DateTime? LockoutEnd { get; set; } // Account lockout time after too many failed attempts
        public string PasswordResetToken { get; set; } // Token for password reset
        public DateTime? PasswordResetTokenExpires { get; set; } // Expiry for the reset token
    }
}
