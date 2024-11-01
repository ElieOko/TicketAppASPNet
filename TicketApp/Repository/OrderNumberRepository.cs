using TicketApp.Data;
using TicketApp.Models;

namespace TicketApp.Repository
{
    public class OrderNumberRepository
    {
        private readonly DataContext _context;
        public OrderNumberRepository(DataContext context) 
        { 
            _context = context;
        }

        public ICollection<OrderNumber> GetAll()
        {
            return _context.OrderNumbers.OrderBy(p => p.OrderNumberId).ToList();
        }
    }
}
