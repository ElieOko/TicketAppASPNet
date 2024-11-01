using TicketApp.Data;
using TicketApp.Models;

namespace TicketApp.Repository
{
    public class TransfertRepository
    {
        private readonly DataContext _context;
        public TransfertRepository(DataContext context) 
        {
            _context = context;
        }

        public ICollection<Transfert> GetAll()
        {
            return _context.Transferts.OrderBy(p => p.TransfertId).ToList();
        }
    }
}
