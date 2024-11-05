using Microsoft.EntityFrameworkCore;
using TicketApp.Data;
using TicketApp.Interfaces;
using TicketApp.Models;

namespace TicketApp.Repository
{
    public class BranchRepository:IBranch
    {
        private readonly DataContext _context;
        public BranchRepository(DataContext context) { 
            _context = context;
        }

        public async Task<ICollection<Branch>> GetAll() 
        {
            return await _context.Branches
               .Include(u => u.transferts)
               .Include(u => u.Users)
               .Include(u => u.orderNumbers)
               .Include(u => u.counters)
               .OrderBy(p => p.BranchId)
               .ToListAsync();
       
        }
        public async Task<bool> Delete(int id) 
        {
            var branch = await _context.Branches.FindAsync(id);
            if (branch == null)
            {
                return false;
            }

            _context.Branches.Remove(branch);
            var result = await _context.SaveChangesAsync();

            return result > 0; 
        }
    }
}


