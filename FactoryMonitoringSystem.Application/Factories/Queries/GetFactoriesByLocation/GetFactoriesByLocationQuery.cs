using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Factories.Queries.GetFactoriesByLocation
{
    public record GetFactoriesByLocationQuery(string location) :IRequest<List<FactoryResponse>>;
    
}
