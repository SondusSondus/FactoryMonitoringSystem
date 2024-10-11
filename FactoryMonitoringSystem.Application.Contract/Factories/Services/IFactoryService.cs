using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Requests;
using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Responses;
using FactoryMonitoringSystem.Domain.Shared;


namespace FactoryMonitoringSystem.Application.Contracts.Factories.Services
{
    public interface IFactoryService
    {
        Task<ErrorOr<Success>> CreateFactoryAsync(FactoryRequest factory, CancellationToken cancellationToken);
        Task<ErrorOr<FactoryResponse>> GetFactoryByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<ErrorOr<List<FactoryResponse>>> GetAllFactoriesAsync(CancellationToken cancellationToken);
        Task<ErrorOr<Success>> UpdateFactoryAsync(UpdateFactoryRequest factoryRequet, CancellationToken cancellationToken);
        Task<ErrorOr<Success>> DeleteFactoryAsync(Guid id, CancellationToken cancellationToken);
        Task<List<FactoryResponse>> GetFactoriesByLocationAsync(string location, CancellationToken cancellationToken);
        Task<List<FactoryWithMachineCountResponse>> GetFactoriesWithMachineCountAsync(CancellationToken cancellationToken);
        Task<string> GenerateFactoryReport(Guid factoryId, CancellationToken cancellationToken);
    }
}
