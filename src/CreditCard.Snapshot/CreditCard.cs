using System;

namespace CreditCard.Snapshot
{
    public class CreditCard
    {
        private Guid _id;
        private Guid _ownerId;
        private decimal? _limit;
        private decimal _avaliableLimit => _limit ?? 0;

        public CreditCard(Guid id, Guid ownerId)
        {
            _id = id;
            _ownerId = ownerId;
        }

        private CreditCard(CreditCardSnapshot snapshot)
        {
            _id = snapshot.Id;
            _ownerId = snapshot.OwnerId;
            _limit = snapshot.AvaliableLimit;
        }

        public static CreditCard CreateFrom(CreditCardSnapshot snapshot) => new CreditCard(snapshot);

        public CreditCardSnapshot GetSnapshot() => new CreditCardSnapshot {
            AvaliableLimit = _avaliableLimit,
            Id = _id,
            OwnerId = _ownerId
        };

        public void AssignLimit(decimal limit)
        {
            if (_limit.HasValue)
            {   
                throw new InvalidOperationException("Limit to the card can be assigned only once");
            }

            _limit = limit;
        }

        public void Withdraw(decimal amount)
        {
            if (amount > _avaliableLimit)
            {
                throw new InvalidOperationException("Lack of funds");
            }
            _limit -= amount;
        }

        public void Repay(decimal amount) => _limit += amount;
    }
}
