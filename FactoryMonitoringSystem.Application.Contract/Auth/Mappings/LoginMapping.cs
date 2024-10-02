

using FactoryMonitoringSystem.Application.Contracts.Auth.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.Common.Mappings;
using FactoryMonitoringSystem.Domain.UsersManagement.Entities;
using Mapster;

namespace FactoryMonitoringSystem.Application.Contracts.Auth.Mappings
{
    public class LoginMapping : IProfile
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<User, LoginResponse>();
        }
    }
}
