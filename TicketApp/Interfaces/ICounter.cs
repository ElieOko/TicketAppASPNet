using TicketApp.Models;

namespace TicketApp.Interfaces
{
    public interface ICounter
    {
        ICollection<Counter> GetAll();
    }
}
