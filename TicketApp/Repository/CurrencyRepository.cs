using Microsoft.EntityFrameworkCore;
using TicketApp.Data;
using TicketApp.Interfaces;
using TicketApp.Models;

namespace TicketApp.Repository
{
    public class CurrencyRepository:ICurrency
    {
        private readonly DataContext _context;
        public CurrencyRepository(DataContext context) 
        {
            _context = context;
        }

        public async Task<ICollection<Currency>> GetAll()
        {
            return await _context.Currencies
                .Include(u=>u.transferts)
                .Include(u => u.intervals)
                .ToListAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var currency = await _context.Currencies.FindAsync(id);
            if (currency == null)
            {
                return false;
            }

            _context.Currencies.Remove(currency);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

    }
}
