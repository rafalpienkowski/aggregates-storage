using System;
using CreditCard.ES.Framework;

namespace CreditCard.ES.Events.AccountOwner
{
    public class AccountOwnerCreated : DomainEvent
    {
        public Guid OwnerId { get; }
        public string OwnerName { get; }

        public AccountOwnerCreated(Guid ownerId, string ownerName)
        {
            OwnerId = ownerId;
            OwnerName = ownerName;
        }
    }
}