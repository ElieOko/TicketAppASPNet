using TicketApp.Data;
using TicketApp.Models;

namespace TicketApp.Repository
{
    public class TitleRepository
    {
        private readonly DataContext _context;
        public TitleRepository(DataContext context) 
        {
            _context = context;  
        }

        public ICollection<Title> GetAll()
        {
            return _context.Titles.OrderBy(p => p.TitleId).ToList();
        }
    }
}
