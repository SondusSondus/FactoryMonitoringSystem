using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Shared.Utilities.GeneralServices
{
    public static class SystemRegularExpression
    {
        public const string Email = @"^[\x21-\x5A\x5E-\x7E]+@((?!-)[A-Za-z0-9-]{1,63}(?<!-)\.)+[A-Za-z]{2,8}$";
        public const string Note = @"^.{0,500}$";

    }
}
