using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CreditCard.Snapshot
{
    public class CreditCardRepository
    {
        private readonly CreditCardContext _context;

        public CreditCardRepository(CreditCardContext context)
        {
            _context = context;
        }

        public async Task Add(CreditCard creditCard)
        {
            var snapshot = creditCard.GetSnapshot();
            await _context.AddAsync(snapshot);
        }

        public async Task<CreditCard> Get(Guid id)
        {
            var creditCardSnapshot = await _context.CreditCards.SingleOrDefaultAsync(cc => cc.Id == id);
            CreditCard creditCard = null;
            if (creditCardSnapshot != null)
            {
                creditCard = CreditCard.CreateFrom(creditCardSnapshot);
            }
            return creditCard;
        }
    }
}
