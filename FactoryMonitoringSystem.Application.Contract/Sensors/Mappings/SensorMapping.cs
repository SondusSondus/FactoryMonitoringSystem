using FactoryMonitoringSystem.Application.Contracts.Common.Mappings;
using FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Request;
using FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Responses;
using FactoryMonitoringSystem.Domain.Sensors.Entities;
using Mapster;

namespace FactoryMonitoringSystem.Application.Contracts.Sensors.Mappings
{
    public class SensorMapping : IProfile
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Sensor, SensorResponse>(); 
            config.NewConfig<SensorRequest, Sensor>();
            config.NewConfig<UpdateSensorRequest, Sensor>();
        }
    }
}
