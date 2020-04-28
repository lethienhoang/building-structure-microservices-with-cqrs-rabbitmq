using Framework.Types;
using System;

namespace Framework.Domain
{
    public abstract class EntityBase : IEntityBase
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }

        protected EntityBase()
        {
            CreatedAt = DateTimeHelper.GenerateTodayUTC();
        }

        protected EntityBase(Guid id)
        {
            Id = id;
        }
    }
}
