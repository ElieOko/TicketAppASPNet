using TicketApp.Data;
using TicketApp.Models;

namespace TicketApp.Repository
{
    public class IntervalRepository
    {
        private readonly DataContext _context;
        public IntervalRepository(DataContext context) 
        {
            _context = context;
        }

        public ICollection<Interval> GetAll()
        {
            return _context.Intervals.OrderBy(p => p.IntervalId).ToList();
        }
    }
}
