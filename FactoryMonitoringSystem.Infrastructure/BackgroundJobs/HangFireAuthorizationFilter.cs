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
            // Get the current HTTP context
            //var httpContext = context.GetHttpContext();

            //// Ensure the user is authenticated
            //if (!httpContext.User.Identity.IsAuthenticated)
            //{
            //    return false; // Deny access if not authenticated
            //}

            //// Check if the user has a specific role (e.g., "Admin")
            //if (httpContext.User.IsInRole("Admin"))
            //{
            //    return true; // Allow access if the user is an admin
            //}

            //// Optionally, allow access based on other criteria
            //var userEmail = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            //if (userEmail == "admin@yourdomain.com")
            //{
            //    return true; // Allow specific user access based on email
            //}

            // Deny access by default
            return false;
        }
    }
}
