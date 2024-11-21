using Microsoft.EntityFrameworkCore;
using TicketApp.Data;
using TicketApp.Interfaces;
using TicketApp.Models;

namespace TicketApp.Repository
{
    public class CustomerRepository:ICustomer
    {
        private readonly DataContext _context;
        public CustomerRepository(DataContext context)
        { 
            _context = context;
        }

        public async Task <ICollection<Customer>> GetAll()
        {
            return await _context.Customers.Include(u=>u.titles).ToListAsync();
        }
        public async Task<bool> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return false;
            }

            _context.Customers.Remove(customer);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}
