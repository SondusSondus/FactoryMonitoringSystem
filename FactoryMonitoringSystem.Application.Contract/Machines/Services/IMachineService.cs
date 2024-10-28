using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.Machines.Models.Requests;
using FactoryMonitoringSystem.Application.Contracts.Machines.Models.Responses;

namespace FactoryMonitoringSystem.Application.Contracts.Machines.Services
{
    public interface IMachineService
    {
        Task<ErrorOr<Success>> CreateMachineAsync(MachineRequest machine, CancellationToken cancellationToken);
        Task<ErrorOr<MachineResponse>> GetMachineByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<ErrorOr<List<MachineResponse>>> GetAllMachinesAsync(CancellationToken cancellationToken);
        Task<ErrorOr<Success>> UpdateMachineAsync(Guid id,MachineRequest machineRequet, CancellationToken cancellationToken);
        Task<ErrorOr<Success>> DeleteMachineAsync(Guid id, CancellationToken cancellationToken);
    }
}
