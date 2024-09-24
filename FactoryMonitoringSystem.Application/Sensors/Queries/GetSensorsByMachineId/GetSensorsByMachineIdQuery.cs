using FactoryMonitoringSystem.Application.Contracts.Sensors.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Sensors.Queries.GetSensorsByMachineId
{
    public record GetSensorsByMachineIdQuery : IRequest<List<SensorResponse>>;
}
