using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Requests;
using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Responses;
using FactoryMonitoringSystem.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Contracts.Factories.Services
{
    public interface IFactoryService
    {
        Task<ErrorOr<Guid>> CreateFactoryAsync(FactoryRequet factory);
        Task<ErrorOr<FactoryResponse>> GetFactoryByIdAsync(Guid id);
        Task<List<FactoryResponse>> GetAllFactoriesAsync();
        Task<bool> UpdateFactoryAsync(Guid id, string name, string location);
        Task<bool> DeleteFactoryAsync(Guid id);
        Task<List<FactoryResponse>> GetFactoriesByLocationAsync(string location);
        Task<List<FactoryWithMachineCountResponse>> GetFactoriesWithMachineCountAsync();
        Task<string> GenerateFactoryReport(Guid factoryId);
    }
}
