using TicketApp.Models;

namespace TicketApp.Interfaces
{
    public interface ITicket
    {
        Task<ICollection<Ticket>> GetAll();
        Task<bool> Delete(int id);
    }
}
