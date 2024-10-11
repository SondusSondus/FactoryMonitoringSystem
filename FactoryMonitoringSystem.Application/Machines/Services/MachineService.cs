using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Machines.Models.Requests;
using FactoryMonitoringSystem.Application.Contracts.Machines.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.Machines.Services;
using FactoryMonitoringSystem.Shared;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using static FactoryMonitoringSystem.Shared.Utilities.Constant.Errors;
using Machine = FactoryMonitoringSystem.Domain.Machines.Entities.Machine;

namespace FactoryMonitoringSystem.Application.Machines.Services
{
    internal class MachineService : ApplicationService<MachineService, Machine>, IMachineService, IScopedDependency
    {

        public MachineService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }


        public async Task<ErrorOr<Success>> CreateMachineAsync(MachineRequest machine, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Creating machine");

            try
            {
                var result = new Machine(GuidGenerator, machine.Name, machine.Type, machine.SerialNumber, machine.FactoryId);
                WriteRepository.Add(result);
                await WriteRepository.SaveChangesAsync(cancellationToken);
                Logger.LogInformation("Machine created successfully: {Name}", machine.Name);
                return Result.Success;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error creating machine {Name}", machine.Name);
                return General.Unexpected; // Return a general error in case of failure

            }
        }

        public async Task<ErrorOr<Success>> DeleteMachineAsync(Guid id, CancellationToken cancellationToken)
        {
            var machine = await ReadRepository.GetByIdAsync(id, cancellationToken);
            if (machine == null)
            {
                Logger.LogError(MachineError.NotFound.Description);
                return MachineError.NotFound;
            }
            Logger.LogInformation("Delete machine {Machine} ", machine.Name);
            WriteRepository.Delete(machine);
            await WriteRepository.SaveChangesAsync(cancellationToken);
            Logger.LogInformation("Machine deleted successfully");
            return Result.Success;
        }

        public async Task<ErrorOr<List<MachineResponse>>> GetAllMachinesAsync(CancellationToken cancellationToken)
        {
            try
            {
                Logger.LogInformation("Retrieve machines");
                var machines = await ReadRepository.GetAllIncludeAsync(cancellationToken, machine => machine.Sensors);
                Logger.LogInformation("Retrieve machines successfully");
                return machines.Adapt<List<MachineResponse>>();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error when fetch Machines");
                Logger.LogInformation(General.Unexpected.Description);
                return General.Unexpected;
            }

        }

        public async Task<ErrorOr<MachineResponse>> GetMachineByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var machine = await ReadRepository.FindIncludeAsync(cancellationToken, machine => machine.Id == id, machine => machine.Sensors);
            if (machine == null)
            {
                Logger.LogError(MachineError.NotFound.Description);
                return MachineError.NotFound; // Return error if factory is not found
            }
            Logger.LogInformation("Fetch machine {Name}", machine.Name);
            Logger.LogInformation("Fetch machine successfully");
            return machine.Adapt<MachineResponse>();
        }

        public async Task<ErrorOr<Success>> UpdateMachineAsync(UpdateMachineRequest machineRequet, CancellationToken cancellationToken)
        {
            var machine = await ReadRepository.GetByIdAsync(machineRequet.Id, cancellationToken);
            if (machine == null)
            {
                Logger.LogError(MachineError.NotFound.Description);
                return MachineError.NotFound;
            }
            Logger.LogInformation("Machine factory {Machine} ", machineRequet.Name);
            machine.Name = machineRequet.Name;
            machine.Type = machineRequet.Type;
            machine.SerialNumber = machineRequet.SerialNumber;
            WriteRepository.Update(machine);
            await WriteRepository.SaveChangesAsync(cancellationToken);
            Logger.LogInformation("Machine updated successfully");
            return Result.Success;
        }
    }
}
