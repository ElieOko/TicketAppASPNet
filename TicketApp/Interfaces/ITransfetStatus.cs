using TicketApp.Models;

namespace TicketApp.Interfaces
{
    public interface ITransfetStatus
    {
        Task<ICollection<TransfertStatus>> GetAll();
        Task<bool> Delete(int id);
    }
}
