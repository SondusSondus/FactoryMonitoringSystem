using ErrorOr;
using FactoryMonitoringSystem.Application.Factories.Commands.CreateFactory;
using FactoryMonitoringSystem.Application.Factories.Commands.UpdateFactor;
using FactoryMonitoringSystem.Application.Factories.Queries.GetFactoriesWithMachineCount;
using FactoryMonitoringSystem.Application.Factories.Queries.GetFactoryById;
using FactoryMonitoringSystem.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FactoryMonitoringSystem.Api.Controllers
{
    namespace FactoriesMonitoringSystem.API.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class FactoriesController : ApiController
        {
        
            [HttpPost]
            public async Task<IActionResult> CreateFactory([FromBody] CreateFactoryCommand command)
            {
                var result = await Mediator.Send(command);
                return result.Match(
                factoryId => RedirectToActionPermanent(
                    actionName: nameof(GetFactoryById),
                    routeValues: new { id = factoryId }), // Success
                Problem); // Error handling
            }

            [HttpPut]
            public async Task<IActionResult> UpdateFactory([FromBody] UpdateFactoryCommand command)
            {
                var result = await Mediator.Send(command);
                return result.Match(
                factoryId => Ok(factoryId),  // Success
                Problem); // Error handling
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetFactoryById(Guid id)
            {
                var query = new GetFactoryByIdQuery(id);
                var factory = await Mediator.Send(query);
                return factory.Match(
                factory => Ok(factory), // Success
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
}
