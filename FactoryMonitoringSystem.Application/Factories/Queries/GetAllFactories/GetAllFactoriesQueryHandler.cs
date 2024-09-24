using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Factories.Queries.GetAllFactories
{
    public class GetAllFactoriesQueryHandler : IRequestHandler<GetAllFactoriesQuery, List<FactoryResponse>>
    {
        public Task<List<FactoryResponse>> Handle(GetAllFactoriesQuery request, CancellationToken cancellationToken)
        {   
            throw new NotImplementedException();
        }
    }
}
