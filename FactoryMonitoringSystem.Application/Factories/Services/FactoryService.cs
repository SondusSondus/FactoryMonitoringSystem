
using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Requests;
using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.Factories.Services;
using FactoryMonitoringSystem.Domain.Common.Repositories;
using FactoryMonitoringSystem.Domain.Factories.Entities;
using FactoryMonitoringSystem.Domain.Factories.Services;
using FactoryMonitoringSystem.Domain.Factories.Specifications;
using FactoryMonitoringSystem.Domain.Shared;
using FactoryMonitoringSystem.Shared;
using FactoryMonitoringSystem.Shared.Utilities;
using FactoryMonitoringSystem.Shared.Utilities.General;
using Microsoft.Extensions.Logging;
using static FactoryMonitoringSystem.Shared.NewFolder.Constant.Errors.Errors;

namespace FactoryMonitoringSystem.Application.Factories.Services
{
    public class FactoryService : IFactoryService, IScopedDependency 

    {
        private readonly IWriteRepository<Factory> _factoryWriteRepository;
        private readonly IReadRepository<Factory> _factoryReadRepository;
        private readonly IFactoryReportService _factoryReportService;
        private readonly IServiceBase<FactoryService> _serviceBase;

        public FactoryService(
            IWriteRepository<Factory> factoryWriteRepository,
            IReadRepository<Factory> factoryReadRepository,
            IFactoryReportService factoryReportService,
            IServiceBase<FactoryService> serviceBase)
        {
            _factoryWriteRepository = factoryWriteRepository;
            _factoryReadRepository = factoryReadRepository;
            _factoryReportService = factoryReportService;
            _serviceBase = serviceBase;
        }

        public async Task<ErrorOr<Guid>> CreateFactoryAsync(FactoryRequet factory)
        {
            _serviceBase.LoggerInfo("Creating factory with {@Factory}", new { factory.Name, factory.Location });

            try
            {
                var result = new Factory(_serviceBase.GuidGenerator(), factory.Name, factory.Location);
                _factoryWriteRepository.Add(result);
                await _factoryWriteRepository.SaveChangesAsync();

                _serviceBase.LoggerInfo("Factory created successfully: {@Factory}", new { factory.Name, factory.Location });

                return result.Id;
            }
            catch (Exception ex)
            {
                _serviceBase.LoggerError(ex, "Error creating factory {@Factory}", new { factory.Name, factory.Location });
                return General.Unexpected; // Return a general error in case of failure

            }

        }

        public async Task<bool> UpdateFactoryAsync(Guid factoryId, string name, string location)
        {
            var factory = await _factoryReadRepository.GetByIdAsync(factoryId);
            if (factory == null) return false;

            factory.UpdateName(name);
            factory.UpdateLocation(location);
            _factoryWriteRepository.Update(factory);
            await _factoryWriteRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteFactoryAsync(Guid factoryId)
        {
            var factory = await _factoryReadRepository.GetByIdAsync(factoryId);
            if (factory == null) return false;

            _factoryWriteRepository.Delete(factory);
            await _factoryWriteRepository.SaveChangesAsync();
            return true;
        }

        public async Task<ErrorOr<FactoryResponse>> GetFactoryByIdAsync(Guid id)
        {
            _serviceBase.LoggerInfo("Fetch factory with  ID {FactoryiD}", id);

            try
            {
                var factory = await _factoryReadRepository.GetByIdAsync(id);
                if (factory == null)
                {
                    _serviceBase.LoggerError("factory with  ID {FactoryiD} is not found ", id);

                    return FactoryError.NotFound; // Return error if factory is not found
                }
                return _serviceBase.GetMapper().Map<FactoryResponse>(factory); // Success: return the factor}

            }
            catch (Exception EX)
            {
                _serviceBase.LoggerError("Exception when fetch factory with  ID {FactoryiD} with exception message {Message}", id, EX.Message);

                return General.Unexpected; // Return a general error in case of failure

            }
        }
        public async Task<List<FactoryResponse>> GetAllFactoriesAsync()
        {
            var factories = await _factoryReadRepository.GetAllAsync();

            return _serviceBase.GetMapper().Map<List<FactoryResponse>>(factories);

        }



        public async Task<List<FactoryResponse>> GetFactoriesByLocationAsync(string location)
        {
            var spec = new FactoryByLocationSpecification(location);
            var factories = await _factoryReadRepository.FindAsync(spec);
            return _serviceBase.GetMapper().Map<List<FactoryResponse>>(factories);

        }

        public async Task<List<FactoryWithMachineCountResponse>> GetFactoriesWithMachineCountAsync()
        {
            var spec = new FactoriesWithMachineCountSpecification();
            var result = await _factoryReadRepository.FindAsync(spec);
            return result.ToList();
        }
        public async Task<string> GenerateFactoryReport(Guid factoryId)
        {
            var factory = await _factoryReadRepository.GetByIdAsync(factoryId);
            if (factory == null) throw new Exception("Factory not found");

            return _factoryReportService.GenerateFactoryReport(factory);
        }
    }
}
