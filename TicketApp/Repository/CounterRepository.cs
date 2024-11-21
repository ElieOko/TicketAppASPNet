using Microsoft.EntityFrameworkCore;
using TicketApp.Data;
using TicketApp.Interfaces;
using TicketApp.Models;

namespace TicketApp.Repository
{
    public class CounterRepository:ICounter
    {
        private readonly DataContext _context;
        public CounterRepository(DataContext context) 
        { 
            _context = context;
        }
        public async Task<ICollection<Counter>> GetAll()
        {
            return await _context.Counters
                .Include(u => u.calls)
                .Include(u => u.branches)
                .ToListAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var counter = await _context.Counters.FindAsync(id);
            if (counter == null)
            {
                return false;
            }

            _context.Counters.Remove(counter);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }


    }
}
