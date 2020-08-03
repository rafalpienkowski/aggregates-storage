using System;

namespace CreditCard.Snapshot
{
    public class AccountOwner
    {
        private Guid _id;
        private string _name;

        public AccountOwner(Guid id, string name)
        {
            _id = id;
            _name = name;
        }

        private AccountOwner(AccountOwnerSnapshot snapshot)
        {
            _id = snapshot.Id;
            _name = snapshot.Name;
        }

        public static AccountOwner CreateFrom(AccountOwnerSnapshot snapshot) => new AccountOwner(snapshot);

        public AccountOwnerSnapshot GetSnapshot() => new AccountOwnerSnapshot
        {
            Id = _id,
            Name = _name
        };
    }
}
