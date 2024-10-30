using FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Sensors.Queries.GetSensorsByMachineId
{
    internal class GetSensorsByMachineIdQueryHandler : IRequestHandler<GetSensorsByMachineIdQuery, List<SensorResponse>>
    {
        public Task<List<SensorResponse>> Handle(GetSensorsByMachineIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
