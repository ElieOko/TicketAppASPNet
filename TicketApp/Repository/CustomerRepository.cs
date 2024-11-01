using TicketApp.Data;
using TicketApp.Models;

namespace TicketApp.Repository
{
    public class CustomerRepository
    {
        private readonly DataContext _context;
        public CustomerRepository(DataContext context)
        { 
            _context = context;
        }

        public ICollection<Customer> GetAll()
        {
            return _context.Customers.OrderBy(p => p.CustomerId).ToList();
        }
    }
}
