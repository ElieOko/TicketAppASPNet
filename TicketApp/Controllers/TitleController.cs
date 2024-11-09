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
    public class TitleController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ITitle _titleRepository;

        public TitleController(DataContext context, ITitle titleRepository)
        {
            _context = context;
            _titleRepository = titleRepository;
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<Title>>>> GetTitles()
        {
            var titles = await _titleRepository.GetAll();
            var response = new ApiResponse<IEnumerable<Title>>
            {
                Success = true,
                Message = "Titles retrieved successfully.",
                Data = titles
            };

            return Ok(response);
        }
        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Title>>> GetTitle(int id)
        {
            var title = await _context.Titles
                .Include(b => b.customers)
                .FirstOrDefaultAsync(b => b.TitleId == id);

            if (title == null)
            {
                return NotFound(new ApiResponse<Title>
                {
                    Success = false,
                    Message = "Title not found",
                    Data = null
                });
            }

            var response = new ApiResponse<Title>
            {
                Success = true,
                Message = "Title created successfully.",
                Data = title
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ApiResponse<Title>> Store(Title title)
        {

            if (string.IsNullOrWhiteSpace(title.TitleName))
                return new ApiResponse<Title>
                {
                    Success = false,
                    Message = "TitleName is required.",
                    Data = null,

                };

            if (await _context.Titles.AnyAsync(b => b.TitleName == title.TitleName))
            {
                return new ApiResponse<Title>
                {
                    Success = false,
                    Message = "A title with this name already exists.",
                    Data = null
                };
            }

            _context.Titles.Add(title);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return new ApiResponse<Title>
                {
                    Success = true,
                    Message = "Title added successfully.",
                    Data = title
                };
            }

            return new ApiResponse<Title>
            {
                Success = false,
                Message = "An error occurred while adding the Title.",
                Data = null
            };
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse<Title>> Update(int id, Title title)
        {
            ModelState.Clear();
            var existingTitle = await _context.Titles.FindAsync(id);

            if (existingTitle == null)
            {
                return new ApiResponse<Title>
                {
                    Success = false,
                    Message = "Title not found.",
                    Data = null
                };
            }

            if (!string.IsNullOrEmpty(title.TitleName))
            {
                existingTitle.TitleName = title.TitleName;
            }

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return new ApiResponse<Title>
                {
                    Success = true,
                    Message = "Title updated successfully.",
                    Data = existingTitle
                };
            }

            return new ApiResponse<Title>
            {
                Success = false,
                Message = "An error occurred while updating the Title.",
                Data = null
            };
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var success = await _titleRepository.Delete(id);
            if (!success)
            {
                return NotFound(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Title not found.",
                    Data = false
                });
            }

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                Message = "Title deleted successfully.",
                Data = true
            });
        }
    }
}
