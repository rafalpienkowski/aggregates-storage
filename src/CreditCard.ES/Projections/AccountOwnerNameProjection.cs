using System;
using CreditCard.ES.Events.AccountOwner;
using Marten.Events.Projections;
using Marten.Schema;

namespace CreditCard.ES.Projections
{
    public class AccountOwnerName
    {
        [Identity]
        public Guid AccountOwnerId { get; set; }
        public string Name { get; set; }
    }

    public class AccountOwnerNameProjection: ViewProjection<AccountOwnerName, Guid>
    {
        public AccountOwnerNameProjection()
        {
            ProjectEvent<ProjectionEvent<AccountOwnerCreated>>(e => e.Data.OwnerId, Persist);
            ProjectEvent<ProjectionEvent<AccountOwnerRenamed>>(e => e.Data.OwnerId, Persist);
        }

        private void Persist(AccountOwnerName view, ProjectionEvent<AccountOwnerCreated> eventData)
        {
            view.Name = eventData.Data.OwnerName;
        }

        private void Persist(AccountOwnerName view, ProjectionEvent<AccountOwnerRenamed> eventData)
        {
            view.Name = eventData.Data.NewName;
        }
    }
}
