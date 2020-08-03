using System;
using CreditCard.ES.Framework;

namespace CreditCard.ES.Events.CreditCard
{
    public class CreditCardCreated : DomainEvent
    {
        public Guid CreditCardId { get; }
        public Guid OwnerId { get; }

        public CreditCardCreated(Guid creditCardId, Guid ownerId)
        {
            CreditCardId = creditCardId;
            OwnerId = ownerId;
        }
    }
}
