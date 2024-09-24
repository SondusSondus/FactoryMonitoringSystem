using Ardalis.Specification;
using FactoryMonitoringSystem.Domain.Common.Specifications;
using FactoryMonitoringSystem.Domain.UsersManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Domain.UsersManagement.Specifications
{
    public class UserWithNameEmailSpecification : BaseSpecification<User>
    {
        public UserWithNameEmailSpecification(string userName, string email)
        {
            Query.Where(user => user.Username == userName || user.Email == email);
        }
    }
}
