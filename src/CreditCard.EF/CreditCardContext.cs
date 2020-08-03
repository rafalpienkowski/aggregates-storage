using Microsoft.EntityFrameworkCore;

namespace CreditCard.EF
{
    public class CreditCardContext : DbContext
    {
        private const string Schema = "ef";

        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<AccountOwner> AccountOwners { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseNpgsql("Host=localhost;Database=aggregates;Username=aggregates;Password=password");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);
            base.OnModelCreating(modelBuilder);
        }
    }
}
