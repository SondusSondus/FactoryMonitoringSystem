namespace FactoryMonitoringSystem.Domain.SensorMachines.Entities
{
    public class TrackingSensorMachineValue
    {
        public Guid Id { get; set; }
        public Guid SensorMachineId { get; set; }
        public double Value { get; set; }
        public DateTime DateOfReadingValue { get; set; }
        public bool IsThresholdBreached { get; set; } = false; // Flag for processing status

    }
}
