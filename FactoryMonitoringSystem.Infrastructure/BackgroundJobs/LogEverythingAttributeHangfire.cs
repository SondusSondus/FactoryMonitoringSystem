using Hangfire.Common;
using Hangfire.Server;
using Serilog;

namespace FactoryMonitoringSystem.Infrastructure.BackgroundJobs
{

    public class LogEverythingAttributeHangfire : JobFilterAttribute, IServerFilter
    {
        private static readonly ILogger Logger = Log.ForContext(typeof(LogEverythingAttributeHangfire));

        public LogEverythingAttributeHangfire()
        {

        }

        public void OnPerforming(PerformingContext context)
        {
            Logger.Information($"Starting job {context.BackgroundJob.Id} ({context.BackgroundJob.Job.Type.Name})");
        }

        public void OnPerformed(PerformedContext context)
        {
            if (context.Exception == null)
            {
                Logger.Information($"Completed job {context.BackgroundJob.Id}");
            }
            else
            {
                Logger.Error($"Job {context.BackgroundJob.Id} failed: {context.Exception.Message}");
            }
        }
    }
}
