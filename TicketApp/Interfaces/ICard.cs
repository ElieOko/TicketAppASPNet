using TicketApp.Models;

namespace TicketApp.Interfaces
{
    public interface ICard
    {
        Task<ICollection<Card>> GetAll();
        Task<bool> Delete(int id);
    }
}
