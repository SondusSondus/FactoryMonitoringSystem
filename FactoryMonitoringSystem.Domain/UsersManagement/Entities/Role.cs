using FactoryMonitoringSystem.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Domain.UsersManagement.Entities
{
    public class Role :BaseEntity<Guid>
    {
        public string RoleName { get; set; }  // Example: Admin, Manager, User

    }
}
