using System.Threading.Tasks;
using TicketApp.Dto;
using TicketApp.Models;

namespace TicketApp.Interfaces
{
    public interface ITicket
    {
        Task<ICollection<TicketDto>> GetAll(); 
        Task<TicketDto?> GetById(int id);
        Task<bool> Delete(int id);
        Task<Ticket?> ChangeStatusTicket(int id, ChangeStatusTicket changeStatusTicket);
        Task<ICollection<TicketDto>> FilterTickets(DateTime? startDate = null, DateTime? endDate = null);
    }
}
