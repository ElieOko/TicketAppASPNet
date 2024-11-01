using TicketApp.Data;
using TicketApp.Models;

namespace TicketApp.Repository
{
    public class CurrencyRepository
    {
        private readonly DataContext _context;
        public CurrencyRepository(DataContext context) 
        {
            _context = context;
        }

        public ICollection<Currency> GetAll()
        {
            return _context.Currencies.OrderBy(p => p.CurrencyId).ToList();
        }

    }
}
