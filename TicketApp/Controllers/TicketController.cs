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
using TicketApp.Dto;
using System.Net.Sockets;
using TicketApp.Services;

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
        public async Task<ActionResult<ApiResponse<IEnumerable<TicketDto>>>> GetTickets()
        {
            var tickets = await _ticketRepository!.GetAll();
            var response = new ApiResponse<IEnumerable<TicketDto>>
            {
                Success = true,
                Message = "Tickets retrieved successfully.",
                Data = tickets
            };
            return Ok(response);
        }

        [Authorize]
        [HttpGet("filter")]
        public async Task<ActionResult<ApiResponse<IEnumerable<TicketDto>>>> FilterTicket(DateTime? startDate = null, DateTime? endDate = null)
        {
            var tickets = await _ticketRepository!.FilterTickets(startDate, endDate);
            var response = new ApiResponse<IEnumerable<TicketDto>>
            {
                Success = true,
                Message = "Tickets retrieved successfully.",
                Data = tickets
            };
            return Ok(response);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TicketDto>>> GetTicket(int id)
        {
            var ticketDto = await _ticketRepository!.GetById(id);

            if (ticketDto == null)
            {
                return NotFound(new ApiResponse<TicketDto> { Success = false, Message = "Ticket not found", Data = null });
            }

            var response = new ApiResponse<TicketDto>
            {
                Success = true,
                Message = "Ticket retrieved successfully.",
                Data = ticketDto
            };

            return Ok(response);
        }
        [Authorize]
        [HttpPost]
        public async Task<ApiResponse<TicketDto>> Store(TicketDto ticketDto)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            _logger.LogInformation("User ID retrieved: {userId}", userId);

            if (string.IsNullOrWhiteSpace(ticketDto.Phone) ||
                string.IsNullOrWhiteSpace(ticketDto.Amount.ToString()) ||
                string.IsNullOrWhiteSpace(ticketDto.CurrencyFId.ToString()) ||
                string.IsNullOrWhiteSpace(ticketDto.TransfertStatusFId.ToString()))
            {
                return new ApiResponse<TicketDto>
                {
                    Success = false,
                    Message = "Phone, Amount, CurrencyFId, TransfertStatusFId are required.",
                    Data = null,
                };
            }


            if (int.TryParse(userId, out int parsedUserId))
            {
                DateTime dateCreated = DateTime.Now;
                var ticket = new Ticket
                {
                    UserFId = parsedUserId,
                    Phone = ticketDto.Phone,
                    Amount = ticketDto.Amount,
                    Motif = ticketDto.Motif,
                    CurrencyFId = ticketDto.CurrencyFId,
                    TransfertStatusFId = ticketDto.TransfertStatusFId,
                    HeureDebut = DateTime.Now.ToString("HH:mm"),
                    TransfertTypeFId = ticketDto.TransfertTypeFId, 
                    DateCreated = dateCreated
                };

                _context!.Tickets.Add(ticket);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    var currency = await _context.Currencies.FindAsync(ticket.CurrencyFId);
                    var transfertStatus = await _context.TransfertsStatus.FindAsync(ticket.TransfertStatusFId);
                    var transferType = await _context.TransferTypes.FindAsync(ticket.TransfertTypeFId);
                    var user= await _context.Users.FindAsync(ticket.UserFId);
                    var userBranch = await _context.Users
                        .Include(u => u.branch) 
                        .FirstOrDefaultAsync(u => u.UserId == ticket.UserFId);
                    _logger.LogInformation("userBranch  retrieved: {userBranch}", userBranch?.branch?.BranchName);

                    var createdTicketDto = new TicketDto
                    {
                        TicketId = ticket.TicketId,
                        Amount = ticket.Amount,
                        Phone = ticket.Phone,
                        Motif = ticket.Motif,
                        DateCreated = ticket.DateCreated,
                        TransfertStatusFId=ticket.TransfertStatusFId,
                        TransfertTypeFId=ticket.TransfertTypeFId,
                        UserFId=ticket.UserFId,
                        CurrencyFId=ticket.CurrencyFId,
                        User = new UserDto
                        {
                            UserId = parsedUserId, 
                            UserName = user?.UserName ?? string.Empty,

                        },
                        TransfertStatus = new TransfertStatusDto
                        {
                            TransferStatusId = ticket.TransfertStatusFId,
                            Name = transfertStatus?.Name ?? string.Empty,
                        },
                        Currency = new CurrencyDto
                        {
                            CurrencyId = ticket.CurrencyFId,
                            CurrencyName = currency?.CurrencyName ?? string.Empty,
                            CurrencyCode = currency?.CurrencyCode ?? string.Empty,
                        },
                        TransferType = new TransfertTypeDto
                        {
                            TransferTypeId = ticket.TransfertTypeFId,
                            Name = transferType?.Name ?? string.Empty,
                        },
                        Branch=new BranchDto
                        {
                            BranchId=userBranch?.branch?.BranchId ?? 0,
                            BranchName= userBranch?.branch?.BranchName ?? string.Empty,
                        }
                    };

                    return new ApiResponse<TicketDto>
                    {
                        Success = true,
                        Message = "Ticket added successfully.",
                        Data = createdTicketDto
                    };
                }
            }

            return new ApiResponse<TicketDto>
            {
                Success = false,
                Message = "An error occurred while adding the Ticket.",
                Data = null
            };
        }
        [Authorize]
        [HttpPut("callTicket/{id}")]
        public async Task<ApiResponse<TicketDto>> CallTicket(int id, TicketDto ticketDto)
        {
            var existingTicket = await _context!.Tickets.FindAsync(id);
            if (existingTicket == null)
            {
                return new ApiResponse<TicketDto>
                {
                    Success = false,
                    Message = "Ticket not found.",
                    Data = null
                };
            }
            TimeSpan? CalculerDuree(string heureDebutString, string? heureFinString)
            { 
                if (TimeSpan.TryParse(heureDebutString, out TimeSpan heureDebut) &&
                    TimeSpan.TryParse(heureFinString, out TimeSpan heureFin))
                {
                    return heureFin - heureDebut;
                }
                else
                {
                    // Log pour indiquer qu'une des heures n'a pas pu être analysée
                    _logger.LogWarning($"Impossible d'analyser les heures. HeureDebut: {heureDebutString}, HeureFin: {heureFinString}");
                    return null;
                }
            }

       

            // Logique pour mettre à jour le ticket
            if (ticketDto.ClotureDateCreated.HasValue)
            {
                existingTicket.ClotureDateCreated = DateTime.Now;
                existingTicket.HeureFin = DateTime.Now.ToString("HH:mm");
                string? heureDebut = existingTicket.HeureDebut;
                string? heureFin = existingTicket.HeureFin;
                _logger.LogWarning($"Impossible d'analyser les heures. HeureDebut: {heureDebut}, HeureFin: {heureFin}");

                // Calculer la durée entre les heures
                TimeSpan? duree = CalculerDuree(heureDebut, heureFin);

                // Si une durée a été calculée, l'affecter au ticket
                if (duree.HasValue)
                {
                    existingTicket.DureeS = duree.Value.ToString(); 
                }
                else
                {
                    existingTicket.DureeS = "Durée non valide"; 
                }
            }


            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                var updatedTicketDto = new TicketDto
                {
                    TicketId = existingTicket.TicketId,
                    Amount = existingTicket.Amount,
                    Phone = existingTicket.Phone,
                    Motif = existingTicket.Motif,
                    DateCreated = existingTicket.DateCreated,
                    ClotureDateCreated = existingTicket.ClotureDateCreated,
                    TransfertStatusFId = existingTicket.TransfertStatusFId,
                    TransfertTypeFId = existingTicket.TransfertTypeFId,
                    UserFId = existingTicket.UserFId,
                    CurrencyFId = existingTicket.CurrencyFId,
                    HeureFin = existingTicket.HeureFin,
                    HeureDebut=existingTicket.HeureDebut
                   

                };

                return new ApiResponse<TicketDto>
                {
                    Success = true,
                    Message = "ClotureDate updated successfully.",
                    Data = updatedTicketDto 
                };
            }

            return new ApiResponse<TicketDto>
            {
                Success = false,
                Message = "An error occurred while updating the Ticket.",
                Data = null
            };
        }
        [Authorize]
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
            if (!string.IsNullOrEmpty(ticket.ClotureDateCreated.ToString()))
            {
                existingTicket.ClotureDateCreated = DateTime.Now;
            }
            if (!string.IsNullOrEmpty(ticket?.transfertStatus?.ToString()))
            {
                existingTicket.transfertStatus = ticket.transfertStatus;
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
        [Authorize]
        [HttpPost("changeTicketStatus/{id}")]
        public async Task<IActionResult> ChangeStatusTicket([FromBody] ChangeStatusTicket changeStatusTicket, int id)
        {
            if (string.IsNullOrWhiteSpace(changeStatusTicket.TransfertStatusFId.ToString()))
            {
                return BadRequest(new ApiResponse<Ticket>
                {
                    Success = false,
                    Message = "Status is required.",
                    Data = null
                });
            }

            var result = await _ticketRepository!.ChangeStatusTicket(id, changeStatusTicket);

            if (result == null)
            {
                var ticket = await _context!.Tickets.FindAsync(id);
                if (ticket == null)
                {
                    return NotFound(new ApiResponse<Ticket>
                    {
                        Success = false,
                        Message = "Ticket non trouvé.",
                        Data = null
                    });
                }
                else if (ticket.StatusChanged)
                {
                    return BadRequest(new ApiResponse<Ticket>
                    {
                        Success = false,
                        Message = "Le statut du ticket a déjà été modifié et ne peut plus être changé.",
                        Data = null
                    });
                }
                else
                {
                    // Erreur inattendue
                    return StatusCode(500, new ApiResponse<Ticket>
                    {
                        Success = false,
                        Message = "Une erreur inattendue est survenue.",
                        Data = null
                    });
                }
            }

            return Ok(new ApiResponse<Ticket>
            {
                Success = true,
                Message = "Status du ticket changé avec succès.",
                Data = result
            });
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
