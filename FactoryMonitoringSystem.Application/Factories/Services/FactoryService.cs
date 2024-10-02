
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
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using static FactoryMonitoringSystem.Shared.Utilities.Constant.Errors;

namespace FactoryMonitoringSystem.Application.Factories.Services
{
    public class FactoryService : ApplicationService<FactoryService, Factory>, IFactoryService, IScopedDependency

    {
        private readonly IFactoryReportService _factoryReportService;

        public FactoryService(IFactoryReportService factoryReportService, IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
            _factoryReportService = factoryReportService;
        }

        public async Task<ErrorOr<Guid>> CreateFactoryAsync(FactoryRequet factory, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Creating factory with {@Factory}", new { factory.Name, factory.Location });

            try
            {
                var result = new Factory(GuidGenerator, factory.Name, factory.Location);
                WriteRepository.Add(result);
                await WriteRepository.SaveChangesAsync(cancellationToken);

                Logger.LogInformation("Factory created successfully: {@Factory}", new { factory.Name, factory.Location });

                return result.Id;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error creating factory {@Factory}", new { factory.Name, factory.Location });
                return General.Unexpected; // Return a general error in case of failure

            }

        }

        public async Task<bool> UpdateFactoryAsync(Guid factoryId, string name, string location, CancellationToken cancellationToken)
        {
            var factory = await ReadRepository.GetByIdAsync(factoryId,cancellationToken);
            if (factory == null) return false;

            factory.UpdateName(name);
            factory.UpdateLocation(location);
            WriteRepository.Update(factory);
            await WriteRepository.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> DeleteFactoryAsync(Guid factoryId, CancellationToken cancellationToken)
        {
            var factory = await ReadRepository.GetByIdAsync(factoryId,cancellationToken);
            if (factory == null) return false;

            WriteRepository.Delete(factory);
            await WriteRepository.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<ErrorOr<FactoryResponse>> GetFactoryByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Fetch factory with  ID {FactoryiD}", id);

            try
            {
                var factory = await ReadRepository.GetByIdAsync(id, cancellationToken);
                if (factory == null)
                {
                    Logger.LogError("factory with  ID {FactoryiD} is not found ", id);

                    return FactoryError.NotFound; // Return error if factory is not found
                }
                return Mapper.Map<FactoryResponse>(factory); // Success: return the factor}

            }
            catch (Exception EX)
            {
                Logger.LogError("Exception when fetch factory with  ID {FactoryiD} with exception message {Message}", id, EX.Message);

                return General.Unexpected; // Return a general error in case of failure

            }
        }
        public async Task<List<FactoryResponse>> GetAllFactoriesAsync(CancellationToken cancellationToken)
        {
            var factories = await ReadRepository.GetAllAsync(cancellationToken);

            return Mapper.Map<List<FactoryResponse>>(factories);

        }



        public async Task<List<FactoryResponse>> GetFactoriesByLocationAsync(string location, CancellationToken cancellationToken)
        {
            var spec = new FactoryByLocationSpecification(location);
            var factories = await ReadRepository.FindAsync(spec, cancellationToken);
            return Mapper.Map<List<FactoryResponse>>(factories);

        }

        public async Task<List<FactoryWithMachineCountResponse>> GetFactoriesWithMachineCountAsync(CancellationToken cancellationToken)
        {
            var spec = new FactoriesWithMachineCountSpecification();
            var result = await ReadRepository.FindAsync(spec, cancellationToken);
            return result.ToList();
        }
        public async Task<string> GenerateFactoryReport(Guid factoryId, CancellationToken cancellationToken)
        {
            var factory = await ReadRepository.GetByIdAsync(factoryId, cancellationToken);
            if (factory == null) throw new Exception("Factory not found");

            return _factoryReportService.GenerateFactoryReport(factory);
        }
    }
}
