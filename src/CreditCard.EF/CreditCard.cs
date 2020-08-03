using System;

namespace CreditCard.EF
{
    public class CreditCard
    {
        public Guid Id { get; private set; }
        public Guid OwnerId { get; private set; }
        public decimal? Limit { get; private set; }
        public decimal AvaliableLimit => Limit.HasValue ? Limit.Value : 0;

        public CreditCard(Guid id, Guid ownerId)
        {
            Id = id;
            OwnerId = ownerId;
        }

        public void AssignLimit(decimal limit)
        {
            if (Limit.HasValue)
            {   
                throw new InvalidOperationException("Limit to the card can be assigned only once");
            }

            Limit = limit;
        }

        public void Withdraw(decimal amount)
        {
            if (amount > AvaliableLimit)
            {
                throw new InvalidOperationException("Lack of funds");
            }

            Limit -= amount;
        }

        public void Repay(decimal amount) => Limit += amount;

    }
}
