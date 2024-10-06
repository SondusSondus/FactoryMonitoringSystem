using FactoryMonitoringSystem.Shared;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Infrastructure.Security.TokenGenerator
{
    internal class ValidateJwtSettings : IValidateOptions<JwtSettings>, ISingletonDependency
    {
        public ValidateOptionsResult Validate(string? name, JwtSettings options)
        {
            if (string.IsNullOrEmpty(options.SecretKey))
            {
                return ValidateOptionsResult.Fail("Secret Key Host is required.");
            }
            if (options.AccessTokenExpirationMinutes <= 0 || options.AccessTokenExpirationMinutes > 30)
            {
                return ValidateOptionsResult.Fail("Access token expiration must be between 0 and 30 ");
            } 
            if (options.RefreshTokenExpirationDays <= 0 || options.RefreshTokenExpirationDays < 1 )
            {
                return ValidateOptionsResult.Fail("Refresh token expiration must be greater  then day ");
            }
            if (string.IsNullOrEmpty(options.Issuer))
            {
                return ValidateOptionsResult.Fail("Issuer is required.");
            }
            if (string.IsNullOrEmpty(options.Audience))
            {
                return ValidateOptionsResult.Fail("Audience is required.");
            }
         
            return ValidateOptionsResult.Success;
        }
    }
}
