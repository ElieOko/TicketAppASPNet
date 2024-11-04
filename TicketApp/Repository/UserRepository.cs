using TicketApp.Data;
using TicketApp.Interfaces;
using TicketApp.Models;

namespace TicketApp.Repository
{
    public class UserRepository : IUser
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
