using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CreditCard.EF
{
    public class AccountOwnerRepository
    {
        private readonly CreditCardContext _context;

        public AccountOwnerRepository(CreditCardContext context)
        {
            _context = context;
        }

        public Task<AccountOwner> Get(Guid id) => _context.AccountOwners.SingleOrDefaultAsync(cc => cc.Id == id);

        public async Task Add(AccountOwner accountOwner)
        {
            await _context.AccountOwners.AddAsync(accountOwner);
        }
    }
}
