using TicketApp.Models;

namespace TicketApp.Interfaces
{
    public interface IBranch
    {
        Task<ICollection<Branch>> GetAll();
        Task<bool> Delete(int id);
    }
}
