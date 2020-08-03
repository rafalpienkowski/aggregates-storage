using System;
using CreditCard.ES.Events.CreditCard;
using CreditCard.ES.Framework;

namespace CreditCard.ES
{

    public class CreditCard : EventSourceAggregate
    {
        private decimal? _limit;
        private Guid? _ownerId;

        public CreditCard(){ }

        public static CreditCard Create(Guid id, Guid ownerId)
        {
            var creditCard = new CreditCard();
            var @event = new CreditCardCreated(id, ownerId);
            creditCard.Apply(@event);
            creditCard.Changes.Add(@event);
            return creditCard;
        }

        public void AssignLimit(decimal limit)
        {
            if (_limit.HasValue)
            {
                return;
            }

            Causes(new CreditCardLimitAssigned(Id, limit));
        }

        public void Withdraw(decimal amount)
        {
            if (_limit.HasValue && amount <= _limit.Value)
            {
                Causes(new CreditCardWithdrawed(Id, amount));
            }
        }

        public void Repay(decimal amount)
        {
            if (_limit.HasValue)
            {
                Causes(new CreditCardRepaid(Id, amount));
            }
        }

        public override void Apply(DomainEvent @event)
        {
            When((dynamic)@event);
            Version++;
        }

        private void When(CreditCardCreated creditCardCreated)
        {
            Id = creditCardCreated.CreditCardId;
            _ownerId = creditCardCreated.OwnerId;
        }

        private void When(CreditCardLimitAssigned limitAssigned)
        {
            _limit = limitAssigned.Limit;
        }

        private void When(CreditCardRepaid creditCardRepaid)
        {
            _limit += creditCardRepaid.Amount;
        }

        private void When(CreditCardWithdrawed creditCardWithdrawed)
        {
            _limit -= creditCardWithdrawed.Amount;
        }
    }
}
