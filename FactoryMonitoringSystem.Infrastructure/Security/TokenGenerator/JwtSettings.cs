using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Infrastructure.Security.TokenGenerator
{
    public record JwtSettings
    {
        public const string Section = "JwtSettings";

        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double AccessTokenExpirationMinutes { get; set; }
        public double RefreshTokenExpirationDays { get; set; }
    }
}
