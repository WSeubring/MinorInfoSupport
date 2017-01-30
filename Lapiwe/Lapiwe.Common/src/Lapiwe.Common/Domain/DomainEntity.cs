using System;

namespace Lapiwe.Common.Domain
{
    public abstract class DomainEntity
    {
        public Guid Guid { get; set; }

        /// <summary>
        /// Create a new entity
        /// </summary>
        public DomainEntity()
        {
            Guid = Guid.NewGuid();
        }

        /// <summary>
        /// Use an existing entity
        /// </summary>
        public DomainEntity(Guid guid)
        {
            Guid = guid;
        }
    }
}
