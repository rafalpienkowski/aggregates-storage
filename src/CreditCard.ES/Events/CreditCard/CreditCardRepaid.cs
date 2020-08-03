using System;
using CreditCard.ES.Framework;

namespace CreditCard.ES.Events.CreditCard
{
    public class CreditCardRepaid : DomainEvent
    {
        public Guid CreditCardId { get; set; }
        public decimal Amount { get; private set;}

        public CreditCardRepaid(Guid creditCardId,decimal amount)
        {
            CreditCardId = creditCardId;
            Amount = amount;
        }
    }
}
