using TicketApp.Models;
using TicketApp.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace TicketApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IBranch _branchRepository;

        public BranchController(DataContext context, IBranch branchRepository)
        {
            _context = context;
            _branchRepository = branchRepository;
        }

        public class ApiResponse<T>
        {
            public bool Success { get; set; }
            public string? Message { get; set; }
            public T? Data { get; set; }
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<Branch>>>> GetBranches()
        {
            var branches = await _branchRepository.GetAll();
            var response = new ApiResponse<IEnumerable<Branch>>
            {
                Success = true,
                Message = "Branches retrieved successfully.",
                Data = branches
            };

            return Ok(response);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Branch>>> GetBranch(int id)
        {
            var branch = await _context.Branches
                .Include(b => b.Users)
                .Include(b => b.transferts)
                .Include(b => b.orderNumbers)
                .Include(b => b.counters)
                .FirstOrDefaultAsync(b => b.BranchId == id);

            if (branch == null)
            {
                return NotFound(new ApiResponse<Branch>
                {
                    Success = false,
                    Message = "Branch not found",
                    Data = null
                });
            }

            var response = new ApiResponse<Branch>
            {
                Success = true,
                Message = "Branch created successfully.",
                Data = branch
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ApiResponse<Branch>> Store(Branch branch)
        {

            if (string.IsNullOrWhiteSpace(branch.BranchName) || string.IsNullOrWhiteSpace(branch.BranchZone))
                return new ApiResponse<Branch>
                {
                    Success = false,
                    Message = "BranchName and BranchZone are required.",
                    Data = null,

                };

            if (await _context.Branches.AnyAsync(b => b.BranchName == branch.BranchName))
            {
                return new ApiResponse<Branch>
                {
                    Success = false,
                    Message = "A branch with this name already exists.",
                    Data = null
                };
            }

            _context.Branches.Add(branch);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return new ApiResponse<Branch>
                {
                    Success = true,
                    Message = "Branch added successfully.",
                    Data = branch
                };
            }

            return new ApiResponse<Branch>
            {
                Success = false,
                Message = "An error occurred while adding the branch.",
                Data = null
            };
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse<Branch>> Update(int id, Branch branch)
        {
            ModelState.Clear();
            var existingBranch = await _context.Branches.FindAsync(id);

            if (existingBranch == null)
            {
                return new ApiResponse<Branch>
                {
                    Success = false,
                    Message = "Branch not found.",
                    Data = null
                };
            }

            if (!string.IsNullOrEmpty(branch.BranchName))
            {
                existingBranch.BranchName = branch.BranchName;
            }

            if (!string.IsNullOrEmpty(branch.BranchZone))
            {
                existingBranch.BranchZone = branch.BranchZone;
            }

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return new ApiResponse<Branch>
                {
                    Success = true,
                    Message = "Branch updated successfully.",
                    Data = existingBranch
                };
            }

            return new ApiResponse<Branch>
            {
                Success = false,
                Message = "An error occurred while updating the branch.",
                Data = null
            };
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var success = await _branchRepository.Delete(id);
            if (!success)
            {
                return NotFound(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Branch not found.",
                    Data = false
                });
            }

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                Message = "Branch deleted successfully.",
                Data = true
            });
        }
    }
}
