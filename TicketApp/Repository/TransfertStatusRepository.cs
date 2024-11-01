using TicketApp.Data;
using TicketApp.Models;

namespace TicketApp.Repository
{
    public class TransfertStatusRepository
    {
        private readonly DataContext _context;
        public TransfertStatusRepository(DataContext context) 
        {  
            _context = context;
        }
        public ICollection<TransfertStatus> GetAll()
        {
            return _context.TransfertsStatus.OrderBy(p => p.TransferStatusId).ToList();
        }
    }
}
