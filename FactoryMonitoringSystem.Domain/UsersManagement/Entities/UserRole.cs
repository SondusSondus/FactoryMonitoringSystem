using FactoryMonitoringSystem.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Domain.UsersManagement.Entities
{
    public class UserRole : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
