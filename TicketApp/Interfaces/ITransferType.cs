using TicketApp.Models;

namespace TicketApp.Interfaces
{
    public interface ITransferType
    {
        Task<ICollection<TransferType>> GetAll();
        Task<bool> Delete(int id);
    }
}
