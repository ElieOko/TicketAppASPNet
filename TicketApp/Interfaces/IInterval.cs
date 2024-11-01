using TicketApp.Models;

namespace TicketApp.Interfaces
{
    public interface IInterval
    {
        ICollection<Interval> GetAll();
    }
}
