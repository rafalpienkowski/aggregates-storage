using System.Threading.Tasks;

namespace CreditCard.Snapshot
{
    public class AccountOwnerRepository
    {
        private readonly CreditCardContext _context;

        public AccountOwnerRepository(CreditCardContext context)
        {
            _context = context;
        }

        public async Task Add(AccountOwner accountOwner)
        {
            var snapshot = accountOwner.GetSnapshot();
            await _context.AddAsync(snapshot);
        }
    }
}
