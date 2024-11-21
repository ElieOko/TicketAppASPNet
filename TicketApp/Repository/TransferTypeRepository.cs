using Microsoft.EntityFrameworkCore;
using TicketApp.Data;
using TicketApp.Interfaces;
using TicketApp.Models;

namespace TicketApp.Repository
{
    public class TransferTypeRepository:ITransferType
    {
        private readonly DataContext _context;
        public TransferTypeRepository(DataContext context) 
        {
            _context = context;
        }

        public async Task<ICollection<TransferType>> GetAll()
        {
            return await _context.TransferTypes
               .Include(u => u.orderNumbers)
               .Include(u => u.intervals)
               .ToListAsync();

        }
        public async Task<bool> Delete(int id)
        {
            var transfertType = await _context.TransferTypes.FindAsync(id);
            if (transfertType == null)
            {
                return false;
            }

            _context.TransferTypes.Remove(transfertType);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}
