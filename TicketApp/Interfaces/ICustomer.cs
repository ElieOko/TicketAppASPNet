using TicketApp.Models;

namespace TicketApp.Interfaces
{
    public interface ICustomer
    {
        ICollection<Customer> GetAll();
    }
}
