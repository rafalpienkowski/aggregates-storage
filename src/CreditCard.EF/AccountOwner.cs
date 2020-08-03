using System;

namespace CreditCard.EF
{
    public class AccountOwner
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public AccountOwner(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
