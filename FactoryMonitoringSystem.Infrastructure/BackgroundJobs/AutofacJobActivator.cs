using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace FactoryMonitoringSystem.Infrastructure.BackgroundJobs
{

    public class AutofacJobActivator : JobActivator
    {
        private readonly IServiceProvider _serviceProvider;

        public AutofacJobActivator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override object ActivateJob(Type jobType)
        {
            // Use the service provider to resolve the job's dependencies
            return _serviceProvider.GetRequiredService(jobType);
        }
    }


}
