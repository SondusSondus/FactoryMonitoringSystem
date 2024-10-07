﻿using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Infrastructure.Persistence.Common
{
    public class WriteDbContextFactory : IDesignTimeDbContextFactory<WriteDbContext>
    {
        public WriteDbContext CreateDbContext(string[] args)
        {
            // Create options builder
            var optionsBuilder = new DbContextOptionsBuilder<WriteDbContext>();

            // Set the connection string or other configurations as per your requirements
            optionsBuilder.UseSqlServer("Server=ICHS-SONDOSSAMA\\SAMARA;Database=FactoriesMonitoring;Connection Timeout=8800;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true;");

            // Return the new WriteDbContext with the options
            return new WriteDbContext(optionsBuilder.Options);
        }
    }
}