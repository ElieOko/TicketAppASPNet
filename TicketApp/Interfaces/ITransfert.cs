using TicketApp.Models;

namespace TicketApp.Interfaces
{
    public interface ITransfert
    {
        ICollection<Transfert> GetAll();
    }
}
