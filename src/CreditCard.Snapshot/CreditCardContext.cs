using Microsoft.EntityFrameworkCore;

namespace CreditCard.Snapshot
{
    public class CreditCardContext : DbContext
    {
        private const string Schema = "snapshot";

        public DbSet<CreditCardSnapshot> CreditCards { get; set; }
        public DbSet<AccountOwnerSnapshot> AccountOwners { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseNpgsql("Host=localhost;Database=aggregates;Username=aggregates;Password=password");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);
            base.OnModelCreating(modelBuilder);
        }
    }
}
