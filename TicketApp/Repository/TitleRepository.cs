using Microsoft.EntityFrameworkCore;
using TicketApp.Data;
using TicketApp.Interfaces;
using TicketApp.Models;

namespace TicketApp.Repository
{
    public class TitleRepository:ITitle
    {
        private readonly DataContext _context;
        public TitleRepository(DataContext context) 
        {
            _context = context;  
        }

        public async Task <ICollection<Title>> GetAll()
        {
            return await _context.Titles.Include(u=>u.customers).ToListAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var title = await _context.Titles.FindAsync(id);
            if (title == null)
            {
                return false;
            }

            _context.Titles.Remove(title);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}
