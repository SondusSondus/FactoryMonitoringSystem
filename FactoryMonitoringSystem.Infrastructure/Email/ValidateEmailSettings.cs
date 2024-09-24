using FactoryMonitoringSystem.Shared;
using FactoryMonitoringSystem.Shared.Utilities.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Infrastructure.Email
{
    public class ValidateEmailSettings : IValidateOptions<EmailSettings>, ISingletonDependency
    {
        public ValidateOptionsResult Validate(string? name, EmailSettings options)
        {
            if (string.IsNullOrEmpty(options.Host))
            {
                return ValidateOptionsResult.Fail("Email Host is required.");
            }
            if (options.Port <= 0)
            {
                return ValidateOptionsResult.Fail("Email Port must be greater than zero.");
            }
            if (string.IsNullOrEmpty(options.Username))
            {
                return ValidateOptionsResult.Fail("Email Username is required.");
            }
            if (string.IsNullOrEmpty(options.Password))
            {
                return ValidateOptionsResult.Fail("Email Password is required.");
            }
            if (string.IsNullOrEmpty(options.FromEmail))
            {
                return ValidateOptionsResult.Fail("FromEmail is required.");
            }
            return ValidateOptionsResult.Success;
        }
    }
}
