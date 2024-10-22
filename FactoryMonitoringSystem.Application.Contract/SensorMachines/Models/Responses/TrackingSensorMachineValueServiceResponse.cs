namespace FactoryMonitoringSystem.Application.Contracts.SensorMachines.Models.Responses
{
    public class TrackingSensorMachineValueServiceResponse
    {
        public Guid Id { get; set; }
        public Guid SensorMachineId { get; set; }
        public double Value { get; set; }
        public DateTime DateOfReadingValue { get; set; }
    }
}
