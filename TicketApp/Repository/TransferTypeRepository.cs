using TicketApp.Data;
using TicketApp.Models;

namespace TicketApp.Repository
{
    public class TransferTypeRepository
    {
        private readonly DataContext _context;
        public TransferTypeRepository(DataContext context) 
        {
            _context = context;
        }

        public ICollection<TransferType> GetAll()
        {
            return _context.TransferTypes.OrderBy(p => p.TransferTypeId).ToList();
        }
    }
}
