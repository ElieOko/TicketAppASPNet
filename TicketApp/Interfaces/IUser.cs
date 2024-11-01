using TicketApp.Models;

namespace TicketApp.Interfaces
{
    public interface IUser
    {
        ICollection<User> GetAll();
    }
}
