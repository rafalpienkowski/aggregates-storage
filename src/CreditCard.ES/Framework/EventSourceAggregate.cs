using System.Collections.Generic;

namespace CreditCard.ES.Framework
{
    public abstract class EventSourceAggregate : Entity
    {
        public List<DomainEvent> Changes { get; private set; }
        public int Version { get; protected set; }
        
        public EventSourceAggregate()
        {
            Changes = new List<DomainEvent>();
        }

        public abstract void Apply(DomainEvent changes);

        protected void Causes(DomainEvent @event)
        {
            Changes.Add(@event);
            Apply(@event);
        }

        public void FlushChanges() => Changes = new List<DomainEvent>();
    }
}
