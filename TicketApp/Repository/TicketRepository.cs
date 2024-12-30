using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using TicketApp.Data;
using TicketApp.Dto;
using TicketApp.Interfaces;
using TicketApp.Models;
using TicketApp.Services;

namespace TicketApp.Repository
{
    public class TicketRepository:ITicket
    {
        private readonly DataContext _context;
        public TicketRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<TicketDto>> FilterTickets(DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _context.Tickets
                .Include(t => t.user)
                .ThenInclude(u => u.branch) // Inclure la branche de l'utilisateur
                .Include(t => t.currency)
                .Include(t => t.transfertStatus)
                .Include(t => t.transferType)
                .AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(t => t.DateCreated!.Value.Date >= startDate.Value.Date);
            }

            if (endDate.HasValue)
            {
                query = query.Where(t => t.DateCreated!.Value.Date <= endDate.Value.Date);
            }

            var tickets = await query.ToListAsync();

            // Mapper les tickets aux DTOs
            var ticketDtos = tickets.Select(ticket => new TicketDto
            {
                TicketId = ticket.TicketId,
                Amount = ticket.Amount,
                Phone = ticket.Phone,
                Motif = ticket.Motif,
                DateCreated = ticket.DateCreated,
                ClotureDateCreated = ticket.ClotureDateCreated,
                HeureDebut = ticket.HeureDebut,
                HeureFin = ticket.ClotureDateCreated.HasValue
                    ? ticket.ClotureDateCreated.Value.ToString("HH:mm")
                    : string.Empty,
                DureeS = ticket.DureeS,

                User = new UserDto
                {
                    UserId = ticket.user?.UserId ?? 0,
                    UserName = ticket.user?.UserName ?? string.Empty,
                },

                TransfertStatus = new TransfertStatusDto
                {
                    TransferStatusId = ticket.transfertStatus?.TransferStatusId ?? 0,
                    Name = ticket.transfertStatus?.Name ?? string.Empty
                },

                Branch = new BranchDto
                {
                    BranchId = ticket.user?.branch?.BranchId ?? 0,
                    BranchName = ticket.user?.branch?.BranchName ?? string.Empty,
                },

                Currency = new CurrencyDto
                {
                    CurrencyId = ticket.currency?.CurrencyId ?? 0,
                    CurrencyName = ticket.currency?.CurrencyName ?? string.Empty,
                    CurrencyCode = ticket.currency?.CurrencyCode ?? string.Empty
                },

                TransferType = new TransfertTypeDto
                {
                    TransferTypeId = ticket.transferType?.TransferTypeId ?? 0,
                    Name = ticket.transferType?.Name ?? string.Empty
                }
            }).ToList();

            return ticketDtos;
        }
        public async Task<ICollection<TicketDto>> GetAll()
        {
           
            var tickets = await _context.Tickets
                .Include(t => t.user)
                .Include(t => t.currency)
                .Include(t => t.transfertStatus)
                .Include(t=> t.branch)
                .Include(t => t.transferType)
                .ToListAsync();

            // Mapper les tickets aux DTOs
            var ticketDtos = tickets.Select(ticket => new TicketDto
            {
                TicketId = ticket.TicketId,
                Amount = ticket.Amount,
                Phone = ticket.Phone,
                Motif = ticket.Motif,
                DateCreated = ticket.DateCreated,
                ClotureDateCreated = ticket.ClotureDateCreated,
                HeureDebut = ticket.HeureDebut,
                HeureFin = ticket.ClotureDateCreated.HasValue
                ? ticket.ClotureDateCreated.Value.ToString("HH:mm")
                : string.Empty,

                User = new UserDto
                {
                    UserId = ticket.user?.UserId ?? 0,
                    UserName = ticket.user?.UserName ?? string.Empty,
                },

                TransfertStatus = new TransfertStatusDto
                {
                    TransferStatusId = ticket.transfertStatus?.TransferStatusId ?? 0,
                    Name = ticket.transfertStatus?.Name ?? string.Empty
                },
                Currency=new CurrencyDto
                {
                    CurrencyId=ticket.currency?.CurrencyId ?? 0,
                    CurrencyName=ticket.currency?.CurrencyName ?? string.Empty,
                    CurrencyCode =ticket.currency?.CurrencyCode ?? string.Empty
                },

                TransferType = new TransfertTypeDto
                {
                    TransferTypeId = ticket.transferType?.TransferTypeId ?? 0,
                    Name = ticket.transferType?.Name ?? string.Empty
                }
            }).ToList();

            return ticketDtos;
        }

        public async Task<TicketDto?> GetById(int id)
        {
            var ticket = await _context.Tickets
                .Include(t => t.user)
                .Include(t => t.currency)
                .Include(t => t.branch)
                .Include(t => t.transfertStatus)
                .Include(t => t.transferType)
                .FirstOrDefaultAsync(t => t.TicketId == id);

            if (ticket == null)
            {
                return null; 
            }

            var ticketDto = new TicketDto
            {
                TicketId = ticket.TicketId,
                Amount = ticket.Amount,
                Phone = ticket.Phone,
                Motif = ticket.Motif,
                DateCreated = ticket.DateCreated,
                ClotureDateCreated = ticket.ClotureDateCreated,
                HeureDebut = ticket.HeureDebut,
                

                User = new UserDto
                {
                    UserId = ticket.user?.UserId ?? 0,
                    UserName = ticket.user?.UserName ?? string.Empty,
                },

                TransfertStatus = new TransfertStatusDto
                {
                    TransferStatusId = ticket.transfertStatus?.TransferStatusId ?? 0,
                    Name = ticket.transfertStatus?.Name ?? string.Empty
                },

                Currency = new CurrencyDto
                {
                    CurrencyId = ticket.currency?.CurrencyId ?? 0,
                    CurrencyName = ticket.currency?.CurrencyName ?? string.Empty,
                    CurrencyCode = ticket.currency?.CurrencyCode ?? string.Empty
                },
                Branch = new BranchDto
                {
                    BranchId = ticket.branch?.BranchId ?? 0,
                    BranchName = ticket.branch?.BranchName ?? string.Empty,
                },


                TransferType = new TransfertTypeDto
                {
                    TransferTypeId = ticket.transferType?.TransferTypeId ?? 0,
                    Name = ticket.transferType?.Name ?? string.Empty
                }
            };

            return ticketDto;
        }

        public async Task<bool> Delete(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return false;
            }

            _context.Tickets.Remove(ticket);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<Ticket?> ChangeStatusTicket(int id, ChangeStatusTicket changeStatusTicket)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return null;

            if (ticket.StatusChanged)
            {
                return null; // Le statut a déjà été modifié, on ne fait rien
            }

            ticket.TransfertStatusFId = changeStatusTicket.TransfertStatusFId;
            ticket.StatusChanged = true; // Marque que le statut a été modifié
            await _context.SaveChangesAsync();
            return ticket;
        }
       
    }
}
