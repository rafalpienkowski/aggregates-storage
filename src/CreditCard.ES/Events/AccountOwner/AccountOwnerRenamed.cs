using System;
using CreditCard.ES.Framework;

namespace CreditCard.ES.Events.AccountOwner
{
    public class AccountOwnerRenamed : DomainEvent
    {
        public Guid OwnerId { get; set; }
        public string NewName { get; set; }

        public AccountOwnerRenamed(Guid ownerId, string newName)
        {
            OwnerId = ownerId;
            NewName = newName;
        }
    }
}