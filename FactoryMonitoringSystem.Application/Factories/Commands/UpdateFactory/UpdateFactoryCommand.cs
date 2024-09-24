using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Requests;
using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Responses;
using MediatR;


namespace FactoryMonitoringSystem.Application.Factories.Commands.UpdateFactor
{
    public record UpdateFactoryCommand(FactoryRequet FactoryRequet) : IRequest<ErrorOr<FactoryResponse>>;
  
}
