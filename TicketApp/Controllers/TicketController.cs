using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static TicketApp.Controllers.BranchController;
using TicketApp.Data;
using TicketApp.Interfaces;
using TicketApp.Models;
using Microsoft.EntityFrameworkCore;
using TicketApp.Repository;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace TicketApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly DataContext? _context;
        private readonly ILogger<TicketController> _logger;
        private readonly ITicket? _ticketRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TicketController(DataContext context, ITicket ticketRepository, ILogger<TicketController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _ticketRepository = ticketRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<Ticket>>>> GetTickets()
        {
            var tickets = await _ticketRepository!.GetAll();
            var response = new ApiResponse<IEnumerable<Ticket>>
            {
                Success = true,
                Message = "Tickets retrieved successfully.",
                Data = tickets
            };

            return Ok(response);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Ticket>>> GetTicket(int id)
        {
            var ticket= await _context!.Tickets
                   .Include(u => u.user)
                   .Include(u => u.currency)
                   .Include(u => u.transfertStatus)
                   .Include(u => u.transferType)
                .FirstOrDefaultAsync(b => b.TicketId == id);

            if (ticket == null)
            {
                return NotFound(new ApiResponse<Ticket>
                {
                    Success = false,
                    Message = "Ticket not found",
                    Data = null
                });
            }

            var response = new ApiResponse<Ticket>
            {
                Success = true,
                Message = "Ticket created successfully.",
                Data = ticket
            };

            return Ok(response);
        }
        [Authorize]
        [HttpPost]
        public async Task<ApiResponse<Ticket>> Store(Ticket ticket)
        {

            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);


            _logger.LogInformation("User ID retrieved: {userId}", userId);
            if (string.IsNullOrWhiteSpace(ticket.Phone) || string.IsNullOrWhiteSpace(ticket.Amount.ToString()) || string.IsNullOrWhiteSpace(ticket.CurrencyFId.ToString()) ||
                string.IsNullOrWhiteSpace(ticket.TransfertStatusFId.ToString()))
                return new ApiResponse<Ticket>
                {
                    Success = false,
                    Message = "Phone, Amount,CurrencyFId,TransfertStatusFId are required.",
                    Data = null,

                };
            
            if (int.TryParse(userId, out int parsedUserId)) 
            {
                ticket.UserFId = parsedUserId;
            }
            else
            {
                return new ApiResponse<Ticket>
                {
                    Success = false,
                    Message = "Invalid user ID.",
                    Data = null
                };
            }

            //if (await _context!.Tickets.AnyAsync(b => b.Code == transfert.Code))
            //{
            //    return new ApiResponse<Ticket>
            //    {
            //        Success = false,
            //        Message = "A transfert with this code already exists.",
            //        Data = null
            //    };
            //}

            _context!.Tickets.Add(ticket);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return new ApiResponse<Ticket>
                {
                    Success = true,
                    Message = "Ticket added successfully.",
                    Data = ticket
                };
            }

            return new ApiResponse<Ticket>
            {
                Success = false,
                Message = "An error occurred while adding the Ticket.",
                Data = null
            };
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse<Ticket>> Update(int id, Ticket ticket)
        {
            ModelState.Clear();
            var existingTicket = await _context!.Tickets.FindAsync(id);

            if (existingTicket == null)
            {
                return new ApiResponse<Ticket>
                {
                    Success = false,
                    Message = "Ticket not found.",
                    Data = null
                };
            }

            if (!string.IsNullOrEmpty(ticket.Phone))
            {
                existingTicket.Phone = ticket.Phone;
            }
            if (!string.IsNullOrEmpty(ticket.Amount.ToString()))
            {
                existingTicket.Amount = ticket.Amount;

            }
            if (!string.IsNullOrEmpty(ticket.Motif))
            {
                existingTicket.Motif = ticket.Motif;
            }

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return new ApiResponse<Ticket>
                {
                    Success = true,
                    Message = "Ticket updated successfully.",
                    Data = existingTicket
                };
            }

            return new ApiResponse<Ticket>
            {
                Success = false,
                Message = "An error occurred while updating the Ticket.",
                Data = null
            };
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var success = await _ticketRepository!.Delete(id);
            if (!success)
            {
                return NotFound(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Ticket not found.",
                    Data = false
                });
            }

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                Message = "Ticket deleted successfully.",
                Data = true
            });
        }
    }
}
