using System;
using CreditCard.ES.Framework;

namespace CreditCard.ES.Events.CreditCard
{
    public class CreditCardLimitAssigned : DomainEvent
    {
        public Guid CreditCardId { get; set; }
        public decimal Limit { get; }

        public CreditCardLimitAssigned(Guid creditCardId, decimal limit)
        {
            CreditCardId = creditCardId;
            Limit = limit;
        }
    }
}
