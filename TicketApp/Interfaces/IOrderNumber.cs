using TicketApp.Models;

namespace TicketApp.Interfaces
{
    public interface IOrderNumber
    {
        ICollection<OrderNumber> GetAll();
    }
}
