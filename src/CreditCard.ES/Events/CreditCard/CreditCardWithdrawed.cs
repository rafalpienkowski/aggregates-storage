using System;
using CreditCard.ES.Framework;

namespace CreditCard.ES.Events.CreditCard
{
    public class CreditCardWithdrawed : DomainEvent
    {
        public Guid CreditCardId { get; set; }
        public decimal Amount { get; private set;}

        public CreditCardWithdrawed(Guid creditCardId, decimal amount)
        {
            CreditCardId = creditCardId;
            Amount = amount;
        }
    }
}
