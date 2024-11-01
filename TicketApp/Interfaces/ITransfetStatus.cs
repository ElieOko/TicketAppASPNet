using TicketApp.Models;

namespace TicketApp.Interfaces
{
    public interface ITransfetStatus
    {
        ICollection<TransfertStatus> GetAll();
    }
}
