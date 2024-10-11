
using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Requests;
using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.Factories.Services;
using FactoryMonitoringSystem.Domain.Factories.Entities;
using FactoryMonitoringSystem.Domain.Factories.Services;
using FactoryMonitoringSystem.Domain.Factories.Specifications;
using FactoryMonitoringSystem.Domain.Shared;
using FactoryMonitoringSystem.Shared;
using Mapster;
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

        public async Task<ErrorOr<Success>> CreateFactoryAsync(FactoryRequest factory, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Creating factory");

            try
            {
                var result = new Factory(GuidGenerator, factory.Name, factory.Location);
                WriteRepository.Add(result);
                await WriteRepository.SaveChangesAsync(cancellationToken);

                Logger.LogInformation("Factory created successfully: {@Factory}", new { factory.Name, factory.Location });

                return Result.Success;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error creating factory {@Factory}", new { factory.Name, factory.Location });
                return General.Unexpected; // Return a general error in case of failure

            }

        }

        public async Task<ErrorOr<Success>> UpdateFactoryAsync(UpdateFactoryRequest factoryRequest, CancellationToken cancellationToken)
        {

            var factory = await ReadRepository.GetByIdAsync(factoryRequest.Id, cancellationToken);
            if (factory == null)
            {
                Logger.LogError(FactoryError.NotFound.Description);
                return FactoryError.NotFound;
            }
            Logger.LogInformation("Update factory {Factory} ", factory.Name);
            factory.Name = factoryRequest.Name;
            factory.Location = factoryRequest.Location;
            WriteRepository.Update(factory);
            await WriteRepository.SaveChangesAsync(cancellationToken);
            Logger.LogInformation("Factory updated successfully");
            return Result.Success;
        }

        public async Task<ErrorOr<Success>> DeleteFactoryAsync(Guid factoryId, CancellationToken cancellationToken)
        {

            var factory = await ReadRepository.GetByIdAsync(factoryId, cancellationToken);
            if (factory == null)
            {
                Logger.LogError(FactoryError.NotFound.Description);
                return FactoryError.NotFound;
            }
            Logger.LogInformation("Delete factory {Factory} ", factory.Name);
            WriteRepository.Delete(factory);
            await WriteRepository.SaveChangesAsync(cancellationToken);
            Logger.LogInformation("Factory deleted successfully");
            return Result.Success;
        }

        public async Task<ErrorOr<FactoryResponse>> GetFactoryByIdAsync(Guid id, CancellationToken cancellationToken)
        {

            var factory = await ReadRepository.GetByIdAsync(id, cancellationToken);
            if (factory == null)
            {
                Logger.LogError(FactoryError.NotFound.Description);

                return FactoryError.NotFound; // Return error if factory is not found
            }
            Logger.LogInformation("Fetch factory {FactoryiD}", factory.Name);
            Logger.LogInformation("Fetch factory successfully");
            return factory.Adapt<FactoryResponse>();
            //return Mapper.Map<FactoryResponse>(factory); // Success: return the factor

        }
        public async Task<ErrorOr<List<FactoryResponse>>> GetAllFactoriesAsync(CancellationToken cancellationToken)
        {

            Logger.LogInformation("Retrieve Factories");
            var factories = await ReadRepository.GetAllAsync(cancellationToken);
            if (!factories.Any())
            {
                Logger.LogInformation(FactoryError.NotFound.Description);
                return FactoryError.NotFound;
            }
            Logger.LogInformation("Retrieve Factories successfully");
            return factories.Adapt<List<FactoryResponse>>();

        }


        public async Task<List<FactoryResponse>> GetFactoriesByLocationAsync(string location, CancellationToken cancellationToken)
        {
            var spec = new FactoryByLocationSpecification(location);
            var factories = await ReadRepository.FindAsync(spec, cancellationToken);
            return factories.Adapt<List<FactoryResponse>>();

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
