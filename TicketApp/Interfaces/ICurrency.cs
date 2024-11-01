using TicketApp.Models;

namespace TicketApp.Interfaces
{
    public interface ICurrency
    {
        ICollection<Currency> GetAll();
    }
}
