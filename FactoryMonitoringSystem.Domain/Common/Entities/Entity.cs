﻿using FactoryMonitoringSystem.Shared.Utilities.Enums;
using System.ComponentModel.DataAnnotations;

namespace FactoryMonitoringSystem.Domain.Common.Entities
{
    public abstract class Entity<T> : BaseEntity
    {
        public T Id { get; set; } // Unique identifier for each entity

      

    }
}