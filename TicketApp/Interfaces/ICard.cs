using TicketApp.Models;

namespace TicketApp.Interfaces
{
    public interface ICard
    {
        ICollection<Card> GetAll();
    }
}
