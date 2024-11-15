﻿using FactoryMonitoringSystem.Application.SensorMachines.Commands.AddSensorToMachine;
using FactoryMonitoringSystem.Application.SensorMachines.Commands.AddValueSensorForMachine;
using FactoryMonitoringSystem.Application.SensorMachines.Queries.GetAllSensorMachine;
using FactoryMonitoringSystem.Application.SensorMachines.Queries.GetSensorMachineById;
using FactoryMonitoringSystem.Application.SensorMachines.Queries.ReadValueForSensorMachine;
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
    public class SensorMachineController : ApiController
    {
        [HttpPost("AddSensorToMachine")]
        public async Task<IActionResult> AddSensorToMachine([FromBody] AddSensorToMachineCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return result.Match(
            value => Ok(), // Success
            Problem); // Error handling
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetSensorMachine([FromQuery] GetSensorMachineByIdQuery query, CancellationToken cancellationToken)
        {
            var sensorMachine = await Mediator.Send(query, cancellationToken);
            return sensorMachine.Match(
            sensorMachine => Ok(sensorMachine), // Success
            Problem); // Error handling
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllSensorMachine(CancellationToken cancellationToken)
        {

            var query = new GetAllSensorMachineQuery();
            var sensorMachines = await Mediator.Send(query, cancellationToken);
            return sensorMachines.Match(
            sensorMachines => Ok(sensorMachines), // Success
            Problem); // Error handling
        }

        [HttpPost("AddTrackingSensorMachineValue")]
        public async Task<IActionResult> AddTrackingSensorMachineValue([FromBody] AddValueSensorForMachineCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return result.Match(
            valus => Ok(), // Success
            Problem); // Error handling
        }

        [HttpGet("GetTrackingSensorMachineValue")]
        public async Task<IActionResult> GetTrackingSensorMachineValue([FromQuery] ReadValueSensorForMachineByIdQuery query, CancellationToken cancellationToken)
        {

            var result = await Mediator.Send(query, cancellationToken);
            return result.Match(
            result => Ok(result), // Success
            Problem); // Error handling
        }

    }
}
