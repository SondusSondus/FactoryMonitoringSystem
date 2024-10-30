using FactoryMonitoringSystem.Domain.SensorMachines.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Infrastructure.Persistence.SensorMachines.Configurations
{
    public class SensorValueOutOfRangeViewConfiguration : IEntityTypeConfiguration<SensorValueOutOfRangeView>
    {
        public void Configure(EntityTypeBuilder<SensorValueOutOfRangeView> builder)
        {
            builder.ToView("View_SensorValuesOutOfRange");
            builder.HasNoKey();
           

        }
    }
}
