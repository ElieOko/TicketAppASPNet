using TicketApp.Models;

namespace TicketApp.Interfaces
{
    public interface IBranch
    {
        ICollection<Branch> GetAll();
    }
}
