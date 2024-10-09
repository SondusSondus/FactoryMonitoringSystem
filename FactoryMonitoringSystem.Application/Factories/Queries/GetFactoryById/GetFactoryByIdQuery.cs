using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Responses;
using MediatR;


namespace FactoryMonitoringSystem.Application.Factories.Queries.GetFactoryById
{
    public record GetFactoryByIdQuery(Guid FactoryId) : IRequest<ErrorOr<FactoryResponse>>;

}
