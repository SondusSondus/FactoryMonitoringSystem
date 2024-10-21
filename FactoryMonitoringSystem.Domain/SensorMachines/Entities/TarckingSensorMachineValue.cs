namespace FactoryMonitoringSystem.Domain.SensorMachines.Entities
{
    public class TarckingSensorMachineValue
    {
        public Guid Id { get; set; }
        public Guid SensorMachineId { get; set; }
        public double Value { get; set; }
        public DateTime DateOfReadingValue { get; set; }
    }
}
