using FactoryMonitoringSystem.Shared.Utilities.Constant;
using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Infrastructure.BackgroundJobs
{
    public class HangFireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {

            var httpContext = context.GetHttpContext();

            // Restrict to authenticated users or specific roles (e.g., Admin)
            return httpContext.User.Identity.IsAuthenticated &&
                   httpContext.User.IsInRole(Roles.Admin);

        }
    }
}
