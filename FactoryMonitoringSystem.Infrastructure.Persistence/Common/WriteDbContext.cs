using FactoryMonitoringSystem.Application.Common.Events;
using FactoryMonitoringSystem.Domain.Common.Entities;
using FactoryMonitoringSystem.Shared.Utilities.Enums;
using FactoryMonitoringSystem.Shared.Utilities.GeneralModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FactoryMonitoringSystem.Infrastructure.Persistence.Common
{
    public class WriteDbContext : DbContextBase<WriteDbContext>
    {
        private readonly IMediator _mediator;

        public WriteDbContext(DbContextOptions<WriteDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;

        }
        public override int SaveChanges()
        {
            TrackEntityChanges();
            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                HandleConcurrencyException(ex);
                throw; // Re-throw after handling the exception
            }
        }
        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            TrackEntityChanges();
           
            try
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                await HandleConcurrencyExceptionAsync(ex);
                throw; // Re-throw after handling the exception
            }
        }
        // Handle concurrency exceptions and notify via domain events
        private void HandleConcurrencyException(DbUpdateConcurrencyException ex)
        {
            foreach (var entry in ex.Entries)
            {
                var entityName = entry.Entity.GetType().Name;
                var userId = GetCurrentUserId();  // Implement this to get the current user's ID

                // Publish domain event for concurrency conflict
                var conflictEvent = new ConcurrencyConflictEvent(new ConcurrencyConflict(entityName,userId));
                _mediator.Publish(conflictEvent).Wait();
            }
        }
        private async Task HandleConcurrencyExceptionAsync(DbUpdateConcurrencyException ex)
        {
            foreach (var entry in ex.Entries)
            {
                var entityName = entry.Entity.GetType().Name;
                var userId = GetCurrentUserId();  // Implement this to get the current user's ID

                // Publish domain event for concurrency conflict
                var conflictEvent = new ConcurrencyConflictEvent(new ConcurrencyConflict(entityName, userId));
                await _mediator.Publish(conflictEvent);
            }
        }

        // Placeholder for retrieving current user's ID
        private string GetCurrentUserId()
        {
            // Implement this to retrieve the current user's ID (e.g., from claims or session)
            return "user@example.com";
        }
        private void TrackEntityChanges()
        {
            var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseEntity<object> &&
                        (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted));

            foreach (var entry in entries)
            {
                var entity = (BaseEntity<object>)entry.Entity;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreatedDate = DateTime.UtcNow;
                        entity.Status = RecordStatus.Active;
                        break;
                    case EntityState.Modified:
                        entity.UpdatedDate=DateTime.UtcNow;
                        entity.Status = entity.Status == RecordStatus.Deleted ? RecordStatus.Deleted : RecordStatus.Active; 
                        break;
                        case EntityState.Deleted:
                        entity.DeletedDate = DateTime.UtcNow;
                        entity.Status = RecordStatus.Deleted;

                        // Convert the deletion to an update (soft delete)
                        entry.State = EntityState.Modified;
                        break;

                }

            }
        }
        // Handle concurrency exceptions and use manual resolution
     
        
    }
}
