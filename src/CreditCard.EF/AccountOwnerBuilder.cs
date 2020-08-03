using System;

namespace CreditCard.EF
{
    internal class AccountOwnerBuilder
    {
        private Guid _id = Guid.NewGuid();
        private string _name = "Dick the Quick";

        public AccountOwnerBuilder WithName(string name)
        {
            _name = name;

            return this;
        }

        public AccountOwner Build() => new AccountOwner(_id, _name);
    }
}