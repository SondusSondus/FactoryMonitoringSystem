using FactoryMonitoringSystem.Api.Controllers;
using FactoryMonitoringSystem.Application.Factories.Commands.CreateFactory;
using FactoryMonitoringSystem.Application.Factories.Commands.DeleteFactory;
using FactoryMonitoringSystem.Application.Factories.Commands.UpdateFactor;
using FactoryMonitoringSystem.Application.Factories.Queries.GetAllFactories;
using FactoryMonitoringSystem.Application.Factories.Queries.GetFactoriesWithMachineCount;
using FactoryMonitoringSystem.Application.Factories.Queries.GetFactoryById;
using FactoryMonitoringSystem.Domain.Shared;
using FactoryMonitoringSystem.Shared.Utilities.Constant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FactoriesMonitoringSystem.API.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    [ApiController]
    [Route("api/[controller]")]
    public class FactoryController : ApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateFactory([FromBody] CreateFactoryCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command,cancellationToken);
            return result.Match(
            factoryId => Ok(), // Success
            Problem); // Error handling
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFactory([FromBody] UpdateFactoryCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return result.Match(
            factory => Ok(),  // Success
            Problem); // Error handling
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetFactory([FromQuery] GetFactoryByIdQuery query, CancellationToken cancellationToken)
        {
            var factory = await Mediator.Send(query, cancellationToken);
            return factory.Match(
            factory => Ok(factory), // Success
            Problem); // Error handling
        }

        [HttpGet()]
        public async Task<IActionResult> GetFactories(CancellationToken cancellationToken)
        {

            var query = new GetAllFactoriesQuery();
            var factory = await Mediator.Send(query, cancellationToken);
            return factory.Match(
            factory => Ok(factory), // Success
            Problem); // Error handling
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteFactory([FromQuery] DeleteFactoryCommand query)
        {
            var factory = await Mediator.Send(query);
            return factory.Match(
            factory => Ok(), // Success
            Problem); // Error handling
        }

        [HttpGet("machine-count")]
        public async Task<ActionResult<List<FactoryWithMachineCountResponse>>> GetFactoriesWithMachineCount()
        {
            var query = new GetFactoriesWithMachineCountQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}

