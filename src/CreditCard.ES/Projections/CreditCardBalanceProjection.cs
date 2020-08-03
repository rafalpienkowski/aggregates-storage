using System;
using CreditCard.ES.Events.AccountOwner;
using CreditCard.ES.Events.CreditCard;
using Marten.Events.Projections;
using Marten.Schema;

namespace CreditCard.ES.Projections
{
    public class CreditCardLimit
    {
        [Identity]
        public Guid CreditCardId { get; set; }
        public Guid AccountOwnerId { get; set; }
        public decimal Limit { get; set; }
        public DateTime LastOperationTimestamp { get; set; }
    }

    public class CreditCardBalanceProjection: ViewProjection<CreditCardLimit, Guid>
    {
        public CreditCardBalanceProjection()
        {
            ProjectEvent<ProjectionEvent<CreditCardCreated>>(e => e.Data.CreditCardId, Persist);
            ProjectEvent<ProjectionEvent<CreditCardLimitAssigned>>(e => e.Data.CreditCardId, Persist);
            ProjectEvent<ProjectionEvent<CreditCardRepaid>>(e => e.Data.CreditCardId, Persist);
            ProjectEvent<ProjectionEvent<CreditCardWithdrawed>>(e => e.Data.CreditCardId, Persist);
        }

        private void Persist(CreditCardLimit view, ProjectionEvent<CreditCardCreated> @event)
        {
            view.AccountOwnerId = @event.Data.OwnerId;
            view.LastOperationTimestamp = @event.Timestamp;
        }

        private void Persist(CreditCardLimit view, ProjectionEvent<CreditCardLimitAssigned> @event)
        {
            view.Limit = @event.Data.Limit;
            view.LastOperationTimestamp = @event.Timestamp;
        }

        private void Persist(CreditCardLimit view, ProjectionEvent<CreditCardRepaid> @event)
        {
            view.Limit += @event.Data.Amount;
            view.LastOperationTimestamp = @event.Timestamp;
        }

        private void Persist(CreditCardLimit view, ProjectionEvent<CreditCardWithdrawed> @event)
        {
            view.Limit -= @event.Data.Amount;
            view.LastOperationTimestamp = @event.Timestamp;
        }
    }
}
