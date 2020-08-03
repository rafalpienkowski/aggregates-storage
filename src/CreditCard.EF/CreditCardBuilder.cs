using System;

namespace CreditCard.EF
{
    internal class CreditCardBuilder
    {
        private Guid _id = Guid.NewGuid();
        private Guid _ownerId = Guid.NewGuid();
        private decimal? _limit;

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

        public CreditCard Build()
        {
            var creditCard = new CreditCard(_id, _ownerId);
            if (_limit.HasValue)
            {
                creditCard.AssignLimit(_limit.Value);
            }

            return creditCard;
        }
    }
}