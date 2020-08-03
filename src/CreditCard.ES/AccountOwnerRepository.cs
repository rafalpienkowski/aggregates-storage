using System;
using Marten;

namespace CreditCard.ES
{
    public class AccountOwnerRepository
    {
        private readonly IDocumentSession _documentSession;

        public AccountOwnerRepository(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public void Add(AccountOwner accountOwner){
            MartenEventStore.CreateNewStream<AccountOwner>(_documentSession, accountOwner.Id, accountOwner.Changes);
            accountOwner.FlushChanges();
        }

        public void Save(AccountOwner accountOwner){
            MartenEventStore.AppendEventsToStream<AccountOwner>(_documentSession, accountOwner.Id, accountOwner.Changes);
            accountOwner.FlushChanges();
        }

        public AccountOwner Get(Guid id) => MartenEventStore.GetStream<AccountOwner>(_documentSession, id);
    }
}
