using TicketApp.Models;

namespace TicketApp.Interfaces
{
    public interface ITitle
    {
        Task<ICollection<Title>> GetAll();
        Task<bool> Delete(int id);
    }
}
