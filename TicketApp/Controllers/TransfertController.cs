//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using TicketApp.Data;
//using TicketApp.Interfaces;
//using static TicketApp.Controllers.BranchController;
//using TicketApp.Models;
//using TicketApp.Repository;

//namespace TicketApp.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TransfertController : ControllerBase
//    {
//        private readonly DataContext ? _context;
//        private readonly ITransfert ? _transfertRepository;

//        public TransfertController(DataContext context, ITransfert transfertRepository)
//        {
//            _context = context;
//            _transfertRepository = transfertRepository;
//        }

//        [Authorize]
//        [HttpGet]
//        public async Task<ActionResult<ApiResponse<IEnumerable<Transfert>>>> GetTransferts()
//        {
//            var transferts = await _transfertRepository!.GetAll();
//            var response = new ApiResponse<IEnumerable<Transfert>>
//            {
//                Success = true,
//                Message = "Transferts retrieved successfully.",
//                Data = transferts
//            };

//            return Ok(response);
//        }
//        [Authorize]
//        [HttpGet("{id}")]
//        public async Task<ActionResult<ApiResponse<Transfert>>> GetTransfert(int id)
//        {
//            var transfert = await _context!.Transferts
//                .Include(b => b.users)
//                .Include(b => b.currencies)
//                .Include(b => b.cards)
//                .Include(b => b.intervals)
//                .Include(b => b.transfertStatus)
//                .Include(b => b.branches)
//                .FirstOrDefaultAsync(b => b.TransfertId == id);

//            if (transfert == null)
//            {
//                return NotFound(new ApiResponse<Transfert>
//                {
//                    Success = false,
//                    Message = "Transfert not found",
//                    Data = null
//                });
//            }

//            var response = new ApiResponse<Transfert>
//            {
//                Success = true,
//                Message = "Transfert created successfully.",
//                Data = transfert
//            };

//            return Ok(response);
//        }

//        [HttpPost]
//        public async Task<ApiResponse<Transfert>> Store(Transfert transfert)
//        {

//            if (string.IsNullOrWhiteSpace(transfert.SenderName) || string.IsNullOrWhiteSpace(transfert.SenderPhone) ||
//                string.IsNullOrWhiteSpace(transfert.ReceiverName) || string.IsNullOrWhiteSpace(transfert.ReceiverPhone) ||
//                string.IsNullOrWhiteSpace(transfert.Address) || string.IsNullOrWhiteSpace(transfert.Code))
//                return new ApiResponse<Transfert>
//                {
//                    Success = false,
//                    Message = "Sender Name, SenderPhone,ReceiverName, ReceiverPhone,Adress,Code and Amount are required.",
//                    Data = null,

//                };

//            if (await _context!.Transferts.AnyAsync(b => b.Code == transfert.Code))
//            {
//                return new ApiResponse<Transfert>
//                {
//                    Success = false,
//                    Message = "A transfert with this code already exists.",
//                    Data = null
//                };
//            }

//            _context.Transferts.Add(transfert);

//            var result = await _context.SaveChangesAsync();

//            if (result > 0)
//            {
//                return new ApiResponse<Transfert>
//                {
//                    Success = true,
//                    Message = "Transfert added successfully.",
//                    Data = transfert
//                };
//            }

//            return new ApiResponse<Transfert>
//            {
//                Success = false,
//                Message = "An error occurred while adding the transfert.",
//                Data = null
//            };
//        }

//        [HttpPut("{id}")]
//        public async Task<ApiResponse<Transfert>> Update(int id, Transfert transfert)
//        {
//            ModelState.Clear();
//            var existingTransfert = await _context!.Transferts.FindAsync(id);

//            if (existingTransfert == null)
//            {
//                return new ApiResponse<Transfert>
//                {
//                    Success = false,
//                    Message = "Transferts not found.",
//                    Data = null
//                };
//            }

//            if (!string.IsNullOrEmpty(transfert.Code))
//            {
//                existingTransfert.Code = transfert.Code;
//            }
//            if (!string.IsNullOrEmpty(transfert.SenderName))
//            {
//                existingTransfert.SenderName = transfert.SenderName;

//            }
//            if (!string.IsNullOrEmpty(transfert.SenderPhone))
//            {
//                existingTransfert.SenderPhone = transfert.SenderPhone;
//            }
//            if (!string.IsNullOrEmpty(transfert.ReceiverName))
//            {
//                existingTransfert.ReceiverName = transfert.ReceiverName;

//            }
//            if (!string.IsNullOrEmpty(transfert.ReceiverPhone))
//            {
//                existingTransfert.ReceiverPhone = transfert.ReceiverPhone;
//            }
//            if (!string.IsNullOrEmpty(transfert.Address))
//            {
//                existingTransfert.Address = transfert.Address;
//            }
//            if (!string.IsNullOrEmpty(transfert.Code))
//            {
//                existingTransfert.Code = transfert.Code;
//            }


//            var result = await _context.SaveChangesAsync();

//            if (result > 0)
//            {
//                return new ApiResponse<Transfert>
//                {
//                    Success = true,
//                    Message = "Transfert updated successfully.",
//                    Data = existingTransfert
//                };
//            }

//            return new ApiResponse<Transfert>
//            {
//                Success = false,
//                Message = "An error occurred while updating the Transfert.",
//                Data = null
//            };
//        }

//        [HttpDelete("{id}")]
//        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
//        {
//            var success = await _transfertRepository!.Delete(id);
//            if (!success)
//            {
//                return NotFound(new ApiResponse<bool>
//                {
//                    Success = false,
//                    Message = "Tranfert not found.",
//                    Data = false
//                });
//            }

//            return Ok(new ApiResponse<bool>
//            {
//                Success = true,
//                Message = "Tranfert deleted successfully.",
//                Data = true
//            });
//        }
//    }
//}
