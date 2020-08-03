using System;
using CreditCard.ES.Events.AccountOwner;
using CreditCard.ES.Framework;

namespace CreditCard.ES
{
    public class AccountOwner : EventSourceAggregate
    {
        private string _name;

        public AccountOwner()
        {
        }

        public static AccountOwner Create(Guid ownerId, string name)
        {
            var accountOwner = new AccountOwner();
            var @event = new AccountOwnerCreated(ownerId, name);
            accountOwner.Apply(@event);
            accountOwner.Changes.Add(@event);

            return accountOwner;
        }

        public void Rename(string newName)
        {
            Causes(new AccountOwnerRenamed(Id, newName));
        }

        public override void Apply(DomainEvent @event)
        {
            When((dynamic)@event);
            Version++;
        }

        private void When(AccountOwnerCreated accountOwnerCreated)
        {
            Id = accountOwnerCreated.OwnerId;
            _name = accountOwnerCreated.OwnerName;
        }

        private void When(AccountOwnerRenamed accountOwnerRenamed)
        {
            _name = accountOwnerRenamed.NewName;
        }
    }
}
