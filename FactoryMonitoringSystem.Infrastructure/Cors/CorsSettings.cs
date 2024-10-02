using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Infrastructure.Cors
{
    public record CorsSettings
    {

        public const string Section = "CorsSettings"; 
        /// <summary>
        /// The internal app allowed cors origins
        /// </summary>
        public List<string> AppAllowedCorsOrigins { get; set; }
        /// <summary>
        /// the external app allowed cors origins
        /// </summary>
        public List<string> ExternalAppAllowedCorsOrigins { get; set; }
        //client code as a key and the list of allowed origins as a value
        public Dictionary<string, List<string>> ClientIpWhiteList { get; set; }
    }
}
