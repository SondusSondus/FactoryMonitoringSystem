using Microsoft.EntityFrameworkCore;


namespace FactoryMonitoringSystem.Infrastructure.Persistence.Common
{
    public class ReadDbContext: DbContextBase<ReadDbContext>
    {
    
        public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

    }
    
    
}
