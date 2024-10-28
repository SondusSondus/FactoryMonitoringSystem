using FactoryMonitoringSystem.Api.Controllers;
using FactoryMonitoringSystem.Application.Contracts.Machines.Models.Requests;
using FactoryMonitoringSystem.Application.Machines.Commands.CreateMachine;
using FactoryMonitoringSystem.Application.Machines.Commands.DeleteMachine;
using FactoryMonitoringSystem.Application.Machines.Commands.UpdateMachine;
using FactoryMonitoringSystem.Application.Machines.Queries.GetAllMachines;
using FactoryMonitoringSystem.Application.Machines.Queries.GetMachineById;
using FactoryMonitoringSystem.Shared.Utilities.Constant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FactoriesMonitoringSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Admin)]
    public class MachineController : ApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateMachine([FromBody] CreateMachineCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return result.Match(
            machineId => Ok(), // Success
            Problem); // Error handling
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateMachine([FromQuery] Guid id, [FromBody] MachineRequest command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new UpdateMachineCommand(id, command), cancellationToken);
            return result.Match(
             machine => Ok(),  // Success
            Problem); // Error handling
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetMachine([FromQuery] GetMachineByIdQuery query, CancellationToken cancellationToken)
        {
            var factory = await Mediator.Send(query, cancellationToken);
            return factory.Match(
             machine => Ok(machine), // Success
            Problem); // Error handling
        }

        [HttpGet()]
        public async Task<IActionResult> GetMachines(CancellationToken cancellationToken)
        {

            var query = new GetAllMachinesQuery();
            var machine = await Mediator.Send(query, cancellationToken);
            return machine.Match(
             machine => Ok(machine), // Success
            Problem); // Error handling
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteMachine([FromQuery] DeleteMachineCommand query)
        {
            var machine = await Mediator.Send(query);
            return machine.Match(
            machine => Ok(), // Success
            Problem); // Error handling
        }


    }
}

