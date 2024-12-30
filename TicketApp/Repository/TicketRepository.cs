using Microsoft.EntityFrameworkCore;
using TicketApp.Data;
using TicketApp.Interfaces;
using TicketApp.Models;

namespace TicketApp.Repository
{
    public class TicketRepository:ITicket
    {
        private readonly DataContext _context;
        public TicketRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Ticket>> GetAll()
        {
            return await _context.Tickets
               .Include(u => u.user)
               .Include(u => u.currency)
               .Include(u => u.transfertStatus)
               .Include(u => u.transferType)
               .ToListAsync();
        }
        public async Task<bool> Delete(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return false;
            }

            _context.Tickets.Remove(ticket);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}
