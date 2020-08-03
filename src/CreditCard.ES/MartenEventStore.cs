using System;
using System.Collections.Generic;
using CreditCard.ES.Framework;
using CreditCard.ES.Projections;
using Marten;

namespace CreditCard.ES
{
    public class MartenEventStore
    {
        private readonly DocumentStore _eventStore;

        public MartenEventStore()
        {
            _eventStore = DocumentStore.For( _ => {
                _.AutoCreateSchemaObjects = AutoCreate.All;
                _.Connection("Host=localhost;Database=aggregates;Username=aggregates;Password=password");
                _.DatabaseSchemaName = "event_store";
                _.Events.InlineProjections.Add<CreditCardBalanceProjection>();
                _.Events.InlineProjections.Add<AccountOwnerNameProjection>();
            });
        }

        public IDocumentSession CreateSession() => _eventStore.OpenSession();

        public static void AppendEventsToStream<T>(IDocumentSession session, Guid entityId, IEnumerable<DomainEvent> changes)
            where T : class
        {
            session.Events.Append(entityId, changes);
            session.SaveChanges();
        }

        public static void CreateNewStream<T>(IDocumentSession session, Guid entityId, IEnumerable<DomainEvent> changes)
            where T : class
        {
            session.Events.StartStream<T>(entityId, changes);
            session.SaveChanges();
        }

        public static T GetStream<T>(IDocumentSession session, Guid entityId)
            where T : EventSourceAggregate, new() => GetStream<T>(session, entityId, 0);

        public static T GetStream<T>(IDocumentSession session, Guid entityId, int version)
            where T : EventSourceAggregate, new()
        {
            var aggregate = new T();
            var events = session.Events.FetchStream(entityId, version);
            foreach(var @event in events)
            {
                aggregate.Apply(@event.Data as DomainEvent);
            }

            return aggregate;
        }
    }
}
