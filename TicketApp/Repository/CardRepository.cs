using Microsoft.EntityFrameworkCore;
using TicketApp.Data;
using TicketApp.Interfaces;
using TicketApp.Models;

namespace TicketApp.Repository
{
    public class CardRepository:ICard
    {
        private readonly DataContext _context;
        public CardRepository(DataContext context) {
            _context = context;
        }

        public async Task<ICollection<Card>> GetAll()
        {
            return await _context.Cards
                .ToListAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return false;
            }

            _context.Cards.Remove(card);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}
