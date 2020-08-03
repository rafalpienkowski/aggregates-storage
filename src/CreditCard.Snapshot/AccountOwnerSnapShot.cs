using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreditCard.Snapshot
{
    public class AccountOwnerSnapshot
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
