using FactoryMonitoringSystem.Shared.Utilities.Enums;
using System.ComponentModel.DataAnnotations;

namespace FactoryMonitoringSystem.Domain.Common.Entities
{
    public abstract class BaseEntity<T>
    {
        public T Id { get; set; } // Unique identifier for each entity

        // Tracking fields
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        // Tracking who performed the actions
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Guid? DeletedBy { get; set; }

        // Entity status
        public RecordStatus Status { get; set; } = RecordStatus.Active;

        // Optimistic concurrency
        [Timestamp]
        public byte[] RowVersion { get; set; }
        // Additional properties for auditing can be added as needed
        // public List<IDomainEvent> DomainEvents { get; } = new();

    }
}
