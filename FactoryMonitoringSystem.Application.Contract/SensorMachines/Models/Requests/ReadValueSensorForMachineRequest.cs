namespace FactoryMonitoringSystem.Application.Contracts.SensorMachines.Models.Requests
{
    public record ReadValueSensorForMachineRequest(Guid SensorMachineId , double Value);


}
