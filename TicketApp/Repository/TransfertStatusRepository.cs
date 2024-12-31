using Microsoft.EntityFrameworkCore;
using TicketApp.Data;
using TicketApp.Interfaces;
using TicketApp.Models;

namespace TicketApp.Repository
{
    public class TransfertStatusRepository:ITransfetStatus
    {
        private readonly DataContext _context;
        public TransfertStatusRepository(DataContext context) 
        {  
            _context = context;
        }

        public async Task<ICollection<TransfertStatus>> GetAll()
        {
            return await _context.TransfertsStatus
               .Include(u => u.tickets)
               .ToListAsync();

        }
        public async Task<bool> Delete(int id)
        {
            var transfertStatus = await _context.TransfertsStatus.FindAsync(id);
            if (transfertStatus == null)
            {
                return false;
            }

            _context.TransfertsStatus.Remove(transfertStatus);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}
