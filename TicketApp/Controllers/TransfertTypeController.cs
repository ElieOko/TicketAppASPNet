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
    public class TransfertTypeController : ControllerBase
    {
        private readonly DataContext? _context;
        private readonly ITransferType? _transfertTypeRepository;

        public TransfertTypeController(DataContext context, ITransferType transfertTypeRepository)
        {
            _context = context;
            _transfertTypeRepository = transfertTypeRepository;
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TransferType>>>> GetTransfertTypes()
        {
            var transferTypes = await _transfertTypeRepository!.GetAll();
            var response = new ApiResponse<IEnumerable<TransferType>>
            {
                Success = true,
                Message = "TransferTypes retrieved successfully.",
                Data = transferTypes
            };

            return Ok(response);
        }
        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TransferType>>> GetTranfertType(int id)
        {
            var tranfertType = await _context!.TransferTypes
                .Include(b => b.orderNumbers)
                .Include(b => b.intervals)
                .FirstOrDefaultAsync(b => b.TransferTypeId == id);

            if (tranfertType == null)
            {
                return NotFound(new ApiResponse<TransferType>
                {
                    Success = false,
                    Message = "TransferType not found",
                    Data = null
                });
            }

            var response = new ApiResponse<TransferType>
            {
                Success = true,
                Message = "TransferType created successfully.",
                Data = tranfertType
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ApiResponse<TransferType>> Store(TransferType transferType)
        {

            if (string.IsNullOrWhiteSpace(transferType.Name) || string.IsNullOrWhiteSpace(transferType.Code))
                return new ApiResponse<TransferType>
                {
                    Success = false,
                    Message = "TransferTypeName and TransferTypeCode are required.",
                    Data = null,

                };

            if (await _context!.TransferTypes.AnyAsync(b => b.Name == transferType.Name))
            {
                return new ApiResponse<TransferType>
                {
                    Success = false,
                    Message = "A TransferType with this name already exists.",
                    Data = null
                };
            }

            _context.TransferTypes.Add(transferType);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return new ApiResponse<TransferType>
                {
                    Success = true,
                    Message = "transferType added successfully.",
                    Data = transferType
                };
            }

            return new ApiResponse<TransferType>
            {
                Success = false,
                Message = "An error occurred while adding the transferType.",
                Data = null
            };
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse<TransferType>> Update(int id, TransferType transferType)
        {
            ModelState.Clear();
            var existingtransferType = await _context!.TransferTypes.FindAsync(id);

            if (existingtransferType == null)
            {
                return new ApiResponse<TransferType>
                {
                    Success = false,
                    Message = "TransferType not found.",
                    Data = null
                };
            }

            if (!string.IsNullOrEmpty(transferType.Name))
            {
                existingtransferType.Name = transferType.Name;
            }

            if (!string.IsNullOrEmpty(transferType.Code))
            {
                existingtransferType.Code = transferType.Code;
            }

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return new ApiResponse<TransferType>
                {
                    Success = true,
                    Message = "TransferType updated successfully.",
                    Data = existingtransferType
                };
            }

            return new ApiResponse<TransferType>
            {
                Success = false,
                Message = "An error occurred while updating the TransferType.",
                Data = null
            };
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var success = await _transfertTypeRepository!.Delete(id);
            if (!success)
            {
                return NotFound(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "TransfertType not found.",
                    Data = false
                });
            }

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                Message = "TransfertType deleted successfully.",
                Data = true
            });
        }
    }
}
