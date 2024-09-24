using Microsoft.AspNetCore.Identity;

namespace FactoryMonitoringSystem.Infrastructure.Security
{
    public class ApplicationUser : IdentityUser
    {
        // Additional custom properties for your users can be added here
        public string FactoryId { get; set; }  // Example: Link users to specific factories
    }

}