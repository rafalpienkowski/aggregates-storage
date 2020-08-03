using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CreditCard.EF
{
    public class CreditCardRepository
    {
        private readonly CreditCardContext _context;

        public CreditCardRepository(CreditCardContext context)
        {
            _context = context;
        }

        public Task<CreditCard> Get(Guid id) => _context.CreditCards.SingleOrDefaultAsync(cc => cc.Id == id);

        public async Task Add(CreditCard creditCard)
        {
            await _context.CreditCards.AddAsync(creditCard);
        }
    }
}
