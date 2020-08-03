using System;
using CreditCard.ES.Events.CreditCard;

namespace CreditCard.ES
{
    internal class CreditCardBuilder
    {
        private Guid _id = Guid.NewGuid();
        private Guid _ownerId = Guid.NewGuid();
        private decimal? _limit;
        private bool _flushEventsAfterBuild = true;

        public CreditCardBuilder WithLimit(decimal limit)
        {
            _limit = limit;

            return this;
        }

        public CreditCardBuilder WithOwnerId(Guid ownerId)
        {
            _ownerId = ownerId;

            return this;
        }

        public CreditCardBuilder DontFlushEvents()
        {
            _flushEventsAfterBuild = false;
            return this;
        }

        public CreditCard Build()
        {
            var creditCard = CreditCard.Create(_id, _ownerId);
            if (_limit.HasValue)
            {
                creditCard.AssignLimit(_limit.Value);
            }

            if(_flushEventsAfterBuild)
            {
                creditCard.FlushChanges();
            }
            

            return creditCard;
        }
    }
}