using TicketApp.Models;

namespace TicketApp.Interfaces
{
    public interface IUser
    {

        Task<ICollection<User>> GetAll();
        Task<User?> GetUser(int id);
        Task<bool> Delete (int id);
        Task<User?> Register(User user);
        Task<User?> ResetPassword(int id, ResetPasswordModel resetPasswordModel); 
    }
}
