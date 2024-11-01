using Microsoft.EntityFrameworkCore;
using TicketApp.Data;
using TicketApp.Models;

namespace TicketApp.Repository
{
    public class CallRepository
    {
        private readonly DataContext _context;
        public CallRepository(DataContext context) {
            _context = context;
        }

        public ICollection<Call> GetAll()
        {
            return _context.Calls.OrderBy(p => p.CallId).ToList();
        }
    }
}
