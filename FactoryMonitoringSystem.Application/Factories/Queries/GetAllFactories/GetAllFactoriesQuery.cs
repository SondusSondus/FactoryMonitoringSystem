using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Responses;
using MediatR;

namespace FactoryMonitoringSystem.Application.Factories.Queries.GetAllFactories
{
    public record GetAllFactoriesQuery : IRequest<ErrorOr<List<FactoryResponse>>>;


}
