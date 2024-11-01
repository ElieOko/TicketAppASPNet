using TicketApp.Data;
using TicketApp.Models;

namespace TicketApp.Repository
{
    public class CardRepository
    {
        private readonly DataContext _context;
        public CardRepository(DataContext context) {
            _context = context;
        }

        public ICollection<Card> GetAll()
        {
            return _context.Cards.OrderBy(p => p.CardId).ToList();
        }
    }
}
