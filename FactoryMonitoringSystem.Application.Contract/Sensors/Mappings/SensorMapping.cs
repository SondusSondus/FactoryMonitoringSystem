using FactoryMonitoringSystem.Application.Contracts.Common.Mappings;
using FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Request;
using FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Responses;
using FactoryMonitoringSystem.Domain.Sensors.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Contracts.Sensors.Mappings
{
    public class SensorMapping : IProfile
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Sensor, SensorResponse>();
            config.NewConfig<SensorRequest, Sensor>();
        }
    }
}
