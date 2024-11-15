using TicketApp.Models;

namespace TicketApp.Interfaces
{
    public interface ICounter
    {
        Task<ICollection<Counter>> GetAll();
        Task<bool> Delete(int id);
    }
}
