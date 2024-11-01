using TicketApp.Data;
using TicketApp.Models;

namespace TicketApp.Repository
{
    public class BranchRepository
    {
        private readonly DataContext _context;
        public BranchRepository(DataContext context) { 
            _context = context;
        }

        public ICollection<Branch> GetAll()
        {
            return _context.Branches.OrderBy(p => p.BranchId).ToList();
        }
    }
}
