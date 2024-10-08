
using FactoryMonitoringSystem.Application.Contracts.Common.Mappings;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Models.Responses;
using FactoryMonitoringSystem.Domain.UsersManagement.Entities;
using Mapster;

namespace FactoryMonitoringSystem.Application.Contracts.UserManagement.Models.Mappings
{
    public record RoleMapping : IProfile
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Role, RoleResponse>();
        }
    }
}
