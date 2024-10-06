using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.Factories.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Factories.Queries.GetFactoriesByLocation
{
    public class GetFactoriesByLocationQueryHandler : IRequestHandler<GetFactoriesByLocationQuery, List<FactoryResponse>>
    {
        private readonly IFactoryService _factoryService;

        public GetFactoriesByLocationQueryHandler(IFactoryService factoryService)
        {
              _factoryService = factoryService;
        }
        public  async Task<List<FactoryResponse>> Handle(GetFactoriesByLocationQuery request, CancellationToken cancellationToken)
        {
            return await _factoryService.GetFactoriesByLocationAsync(request.location, cancellationToken);
        }
    }
}
