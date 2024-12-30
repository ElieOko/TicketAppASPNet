//using Microsoft.EntityFrameworkCore;
//using TicketApp.Data;
//using TicketApp.Interfaces;
//using TicketApp.Models;

//namespace TicketApp.Repository
//{
//    public class TransfertRepository:ITransfert
//    {
//        private readonly DataContext _context;
//        public TransfertRepository(DataContext context)
//        {
//            _context = context;
//        }
//        public async Task<ICollection<Transfert>> GetAll()
//        {
//            return await _context.Transferts
//               .Include(u => u.users)
//               .Include(u => u.currencies)
//               .Include(u => u.cards)
//               .Include(u => u.intervals)
//               .Include(u => u.transfertStatus)
//               .Include(u => u.branches)
//               .ToListAsync();
//        }
//        public async Task<bool> Delete(int id)
//        {
//            var transfert = await _context.Transferts.FindAsync(id);
//            if (transfert == null)
//            {
//                return false;
//            }

//            _context.Transferts.Remove(transfert);
//            var result = await _context.SaveChangesAsync();

//            return result > 0;
//        }
//    }
//}
