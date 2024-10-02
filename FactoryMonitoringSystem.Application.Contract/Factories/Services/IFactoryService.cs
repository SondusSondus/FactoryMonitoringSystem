using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Requests;
using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Responses;
using FactoryMonitoringSystem.Domain.Shared;


namespace FactoryMonitoringSystem.Application.Contracts.Factories.Services
{
    public interface IFactoryService
    {
        Task<ErrorOr<Guid>> CreateFactoryAsync(FactoryRequet factory, CancellationToken cancellationToken);
        Task<ErrorOr<FactoryResponse>> GetFactoryByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<FactoryResponse>> GetAllFactoriesAsync(CancellationToken cancellationToken);
        Task<bool> UpdateFactoryAsync(Guid id, string name, string location, CancellationToken cancellationToken);
        Task<bool> DeleteFactoryAsync(Guid id, CancellationToken cancellationToken);
        Task<List<FactoryResponse>> GetFactoriesByLocationAsync(string location, CancellationToken cancellationToken);
        Task<List<FactoryWithMachineCountResponse>> GetFactoriesWithMachineCountAsync(CancellationToken cancellationToken);
        Task<string> GenerateFactoryReport(Guid factoryId, CancellationToken cancellationToken);
    }
}
