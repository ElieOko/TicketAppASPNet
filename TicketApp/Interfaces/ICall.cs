using TicketApp.Models;

namespace TicketApp.Interfaces
{
    public interface ICall
    {
        ICollection<Call> GetCalls();
    }
}
