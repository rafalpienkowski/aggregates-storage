using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreditCard.Snapshot
{
    public class CreditCardSnapshot
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public decimal? AvaliableLimit { get; set; }
    }
}
