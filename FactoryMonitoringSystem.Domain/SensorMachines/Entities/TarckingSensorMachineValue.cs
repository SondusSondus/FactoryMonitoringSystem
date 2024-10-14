namespace FactoryMonitoringSystem.Domain.SensorMachines.Entities
{
    public class TarckingSensorMachineValue
    {
        public Guid SensorMachineId { get; set; }
        public double SensorMachineValue { get; set; }
        public DateTime DateOfReadingValue { get; set; }
        virtual public SensorMachine SensorMachine { get; set; }
    }
}
