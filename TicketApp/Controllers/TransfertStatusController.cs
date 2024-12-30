using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketApp.Data;
using TicketApp.Interfaces;
using static TicketApp.Controllers.BranchController;
using TicketApp.Models;
using TicketApp.Repository;

namespace TicketApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransfertStatusController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ITransfetStatus _TranfertStatusRepository;

        public TransfertStatusController(DataContext context, ITransfetStatus tranfertStatusRepository)
        {
            _context = context;
            _TranfertStatusRepository = tranfertStatusRepository;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TransfertStatus>>>> GetTransfertStatus()
        {
            var transfertStatus = await _TranfertStatusRepository.GetAll();
            var response = new ApiResponse<IEnumerable<TransfertStatus>>
            {
                Success = true,
                Message = "TransfertStatus retrieved successfully.",
                Data = transfertStatus
            };

            return Ok(response);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TransfertStatus>>> GetTransfertStatus(int id)
        {
            var transfertStatus = await _context.TransfertsStatus
                .Include(b => b.tickets)
                .FirstOrDefaultAsync(b => b.TransferStatusId == id);

            if (transfertStatus == null)
            {
                return NotFound(new ApiResponse<TransfertStatus>
                {
                    Success = false,
                    Message = "transfertStatus not found",
                    Data = null
                });
            }

            var response = new ApiResponse<TransfertStatus>
            {
                Success = true,
                Message = "transfertStatus created successfully.",
                Data = transfertStatus
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ApiResponse<TransfertStatus>> Store(TransfertStatus transfertStatus)
        {

            if (string.IsNullOrWhiteSpace(transfertStatus.Name))
                return new ApiResponse<TransfertStatus>
                {
                    Success = false,
                    Message = "TransfertStatus is required.",
                    Data = null,

                };

            if (await _context.TransfertsStatus.AnyAsync(b => b.Name == transfertStatus.Name))
            {
                return new ApiResponse<TransfertStatus>
                {
                    Success = false,
                    Message = "A TransfertStatus with this name already exists.",
                    Data = null
                };
            }

            _context.TransfertsStatus.Add(transfertStatus);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return new ApiResponse<TransfertStatus>
                {
                    Success = true,
                    Message = "transfertStatus added successfully.",
                    Data = transfertStatus
                };
            }

            return new ApiResponse<TransfertStatus>
            {
                Success = false,
                Message = "An error occurred while adding the transfertStatus.",
                Data = null
            };
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse<TransfertStatus>> Update(int id, TransfertStatus transfertStatus)
        {
            ModelState.Clear();
            var existingTransfertStatus = await _context.TransfertsStatus.FindAsync(id);

            if (existingTransfertStatus == null)
            {
                return new ApiResponse<TransfertStatus>
                {
                    Success = false,
                    Message = "TransfertStatus not found.",
                    Data = null
                };
            }

            if (!string.IsNullOrEmpty(transfertStatus.Name))
            {
                existingTransfertStatus.Name = transfertStatus.Name;
            }

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return new ApiResponse<TransfertStatus>
                {
                    Success = true,
                    Message = "TransfertStatus updated successfully.",
                    Data = existingTransfertStatus
                };
            }

            return new ApiResponse<TransfertStatus>
            {
                Success = false,
                Message = "An error occurred while updating the TransfertStatus.",
                Data = null
            };
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var success = await _TranfertStatusRepository.Delete(id);
            if (!success)
            {
                return NotFound(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "TranfertStatus not found.",
                    Data = false
                });
            }

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                Message = "TranfertStatus deleted successfully.",
                Data = true
            });
        }
    }
}
