using Microsoft.EntityFrameworkCore;
using TicketApp.Data;
using TicketApp.Interfaces;
using TicketApp.Models;
using TicketApp.Services;

namespace TicketApp.Repository
{
    public class UserRepository : IUser
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context) 
        {
            _context = context;
        } 

        public async Task<ICollection<User>> GetAll()
        {

            return await _context.Users
                .Include(u => u.tickets) 
                .Include(u => u.calls)
                .Include(u => u.branch)
                .OrderBy(p => p.UserId)
                .ToListAsync();
        }
        public async Task<User?> GetUser(int id)
        {
            return await _context.Users
                .Include(u => u.tickets) 
                .Include(u => u.calls)
                .Include(u => u.branch)
                .OrderBy(u => u.UserId)
                .FirstOrDefaultAsync(u => u.UserId == id); 
        }
        public async Task<bool> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            var result = await _context.SaveChangesAsync();

            return result > 0;

        }
        public async Task<User?> Register(User user)
        {
            if (await _context.Users.AnyAsync(u => u.UserName == user.UserName))
                return null; 

            PaaswordHasher.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var newUser = new User
            {
                UserName = user.UserName,
                Password = Convert.ToBase64String(passwordHash),
                FullName = user.FullName,
                Locked = user.Locked,
                AccessLevel = user.AccessLevel,
                UserSalt = Convert.ToBase64String(passwordSalt),
                MaxAttempt = user.MaxAttempt,
                isAdmin = user.isAdmin,
                BranchFId = user.BranchFId

            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return newUser; 
        }

        public async Task<User?> ResetPassword(int id, ResetPasswordModel resetPasswordModel)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            PaaswordHasher.CreatePasswordHash(resetPasswordModel.NewPassword, out byte[] newPasswordHash, out byte[] newPasswordSalt);
            user.Password = Convert.ToBase64String(newPasswordHash);
            user.UserSalt = Convert.ToBase64String(newPasswordSalt);
            user.MaxAttempt = 0;
            user.Locked=false;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user; 
        }
    }



}
