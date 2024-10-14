using FactoryMonitoringSystem.Shared;
using Hangfire;

namespace FactoryMonitoringSystem.Infrastructure.Notifications
{
    public interface IMonitoringTaskScheduler
    {
        void ScheduleMonitoringTasks();
    }
    public class MonitoringTaskScheduler : IMonitoringTaskScheduler  , ISingletonDependency
    {
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly MachineMonitoringService _machineService;

        public MonitoringTaskScheduler(
            IRecurringJobManager recurringJobManager,
            MachineMonitoringService machineService)
        {
            _recurringJobManager = recurringJobManager;
            _machineService = machineService;
        }

        public void ScheduleMonitoringTasks()
        {

            _recurringJobManager.AddOrUpdate("CheckMachineStatusAsync", () => _machineService.CheckMachineStatusAsync(), Cron.Hourly);
        }
    }

}
