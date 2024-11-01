using TicketApp.Data;
using TicketApp.Models;

namespace TicketApp.Repository
{
    public class CounterRepository
    {
        private readonly DataContext _context;
        public CounterRepository(DataContext context) 
        { 
            _context = context;
        }

        public ICollection<Counter> GetAll()
        {
            return _context.Counters.OrderBy(p => p.CounterId).ToList();
        }
    }
}
