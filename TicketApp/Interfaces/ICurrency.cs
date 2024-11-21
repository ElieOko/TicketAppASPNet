using TicketApp.Models;

namespace TicketApp.Interfaces
{
    public interface ICurrency
    {
        Task<ICollection<Currency>> GetAll();
        Task<bool> Delete(int id);
    }
}
