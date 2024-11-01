using TicketApp.Models;

namespace TicketApp.Interfaces
{
    public interface ITitle
    {
        ICollection<Title> GetAll();
    }
}
