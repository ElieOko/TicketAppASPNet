using TicketApp.Data;
using TicketApp.Models;

namespace TicketApp.Repository
{
    public class UserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context) 
        {
            _context = context;
        }

        public ICollection<User> GetAll()
        {
            return _context.Users.OrderBy(p => p.UserId).ToList();
        }
    }
}
