using FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Request;
using FactoryMonitoringSystem.Application.Sensors.Commands.AddSensorToMachine;
using FactoryMonitoringSystem.Application.Sensors.Commands.DeleteSensor;
using FactoryMonitoringSystem.Application.Sensors.Commands.UpdateSensor;
using FactoryMonitoringSystem.Application.Sensors.Queries.GetAllSensors;
using FactoryMonitoringSystem.Application.Sensors.Queries.GetSensorById;
using FactoryMonitoringSystem.Shared.Utilities.Constant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FactoryMonitoringSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Admin)]
    public class SensorController : ApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateSensor([FromBody] CreateSensorCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return result.Match(
            sensor => Ok(), // Success
            Problem); // Error handling
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateSensor([FromQuery] Guid id,[FromBody] SensorRequest command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new UpdateSensorCommand(id,command), cancellationToken);
            return result.Match(
            sensor => Ok(),  // Success
            Problem); // Error handling
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetSensor([FromQuery] GetSensorByIdQuery query, CancellationToken cancellationToken)
        {
            var sensor = await Mediator.Send(query, cancellationToken);
            return sensor.Match(
            sensor => Ok(sensor), // Success
            Problem); // Error handling
        }

        [HttpGet()]
        public async Task<IActionResult> GetSensors(CancellationToken cancellationToken)
        {

            var query = new GetAllSensorsQuery();
            var sensors = await Mediator.Send(query, cancellationToken);
            return sensors.Match(
            sensor => Ok(sensor), // Success
            Problem); // Error handling
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteSensor([FromQuery] DeleteSensorCommand query)
        {
            var sensor = await Mediator.Send(query);
            return sensor.Match(
            sensor => Ok(), // Success
            Problem); // Error handling
        }


    }
}
