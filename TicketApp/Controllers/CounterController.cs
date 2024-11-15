using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static TicketApp.Controllers.BranchController;
using TicketApp.Data;
using TicketApp.Interfaces;
using TicketApp.Models;
using Microsoft.EntityFrameworkCore;
using TicketApp.Repository;

namespace TicketApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CounterController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ICounter _counterRepository;

        public CounterController(DataContext context, ICounter counterRepository)
        {
            _context = context;
            _counterRepository = counterRepository;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<Counter>>>> GetCounters()
        {
            var counters = await _counterRepository.GetAll();
            var response = new ApiResponse<IEnumerable<Counter>>
            {
                Success = true,
                Message = "Counters retrieved successfully.",
                Data = counters
            };

            return Ok(response);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Counter>>> GetCounter(int id)
        {
            var counter = await _context.Counters
                .Include(u => u.calls)
                .Include(u => u.branches)
                .FirstOrDefaultAsync(b => b.CounterId == id);

            if (counter == null)
            {
                return NotFound(new ApiResponse<Counter>
                {
                    Success = false,
                    Message = "counter not found",
                    Data = null
                });
            }

            var response = new ApiResponse<Counter>
            {
                Success = true,
                Message = "Counter created successfully.",
                Data = counter
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ApiResponse<Counter>> Store(Counter counter)
        {

            if (string.IsNullOrWhiteSpace(counter.CounterName))
                return new ApiResponse<Counter>
                {
                    Success = false,
                    Message = "CounterName is required.",
                    Data = null,

                };

            if (await _context.Counters.AnyAsync(b => b.CounterName == counter.CounterName))
            {
                return new ApiResponse<Counter>
                {
                    Success = false,
                    Message = "A Counter with this name already exists.",
                    Data = null
                };
            }

            _context.Counters.Add(counter);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return new ApiResponse<Counter>
                {
                    Success = true,
                    Message = "Counter added successfully.",
                    Data = counter
                };
            }

            return new ApiResponse<Counter>
            {
                Success = false,
                Message = "An error occurred while adding the Counter.",
                Data = null
            };
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse<Counter>> Update(int id, Counter counter)
        {
            ModelState.Clear();
            var existingCounter = await _context.Counters.FindAsync(id);

            if (existingCounter == null)
            {
                return new ApiResponse<Counter>
                {
                    Success = false,
                    Message = "Counter not found.",
                    Data = null
                };
            }

            if (!string.IsNullOrEmpty(existingCounter.CounterName))
            {
                existingCounter.CounterName = counter.CounterName;
            }

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return new ApiResponse<Counter>
                {
                    Success = true,
                    Message = "Counter updated successfully.",
                    Data = existingCounter
                };
            }

            return new ApiResponse<Counter>
            {
                Success = false,
                Message = "An error occurred while updating the Counter.",
                Data = null
            };
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var success = await _counterRepository.Delete(id);
            if (!success)
            {
                return NotFound(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Counter not found.",
                    Data = false
                });
            }

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                Message = "Counter deleted successfully.",
                Data = true
            });
        }
    }
}
