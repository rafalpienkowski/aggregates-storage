using System;
using Marten;

namespace CreditCard.ES
{
    public class CreditCardRepository
    {
        private readonly IDocumentSession _session;

        public CreditCardRepository(IDocumentSession session)
        {
            _session = session;
        }

        public void Add(CreditCard creditCard){
            MartenEventStore.CreateNewStream<CreditCard>(_session, creditCard.Id, creditCard.Changes);
            creditCard.FlushChanges();
        }

        public void Save(CreditCard creditCard){
            MartenEventStore.AppendEventsToStream<CreditCard>(_session, creditCard.Id, creditCard.Changes);
            creditCard.FlushChanges();
        }

        public CreditCard Get(Guid id) => MartenEventStore.GetStream<CreditCard>(_session, id);
    }
}
