using TicketApp.Models;

namespace TicketApp.Interfaces
{
    public interface ICustomer
    {
        Task<ICollection<Customer>> GetAll();
        Task<bool> Delete(int id);
    }
}
