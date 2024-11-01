using TicketApp.Models;

namespace TicketApp.Interfaces
{
    public interface ITransferType
    {
        ICollection<TransferType> GetAll();
    }
}
