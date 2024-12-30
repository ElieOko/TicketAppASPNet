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
    public class CardController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ICard _cardRepository;

        public CardController(DataContext context, ICard cardRepository)
        {
            _context = context;
            _cardRepository = cardRepository;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<Card>>>> GetGards()
        {
            var cards = await _cardRepository.GetAll();
            var response = new ApiResponse<IEnumerable<Card>>
            {
                Success = true,
                Message = "Cards retrieved successfully.",
                Data = cards
            };

            return Ok(response);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Card>>> GetCard(int id)
        {
            var card = await _context.Cards
                .FirstOrDefaultAsync(b => b.CardId == id);

            if (card == null)
            {
                return NotFound(new ApiResponse<Card>
                {
                    Success = false,
                    Message = "Card not found",
                    Data = null
                });
            }

            var response = new ApiResponse<Card>
            {
                Success = true,
                Message = "Card created successfully.",
                Data = card
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ApiResponse<Card>> Store(Card card)
        {

            if (string.IsNullOrWhiteSpace(card.CardName))
                return new ApiResponse<Card>
                {
                    Success = false,
                    Message = "CardName is required.",
                    Data = null,

                };

            if (await _context.Cards.AnyAsync(b => b.CardName == card.CardName))
            {
                return new ApiResponse<Card>
                {
                    Success = false,
                    Message = "A Card with this name already exists.",
                    Data = null
                };
            }

            _context.Cards.Add(card);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return new ApiResponse<Card>
                {
                    Success = true,
                    Message = "Card added successfully.",
                    Data = card
                };
            }

            return new ApiResponse<Card>
            {
                Success = false,
                Message = "An error occurred while adding the card.",
                Data = null
            };
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse<Card>> Update(int id, Card card)
        {
            ModelState.Clear();
            var existingCard = await _context.Cards.FindAsync(id);

            if (existingCard == null)
            {
                return new ApiResponse<Card>
                {
                    Success = false,
                    Message = "Card not found.",
                    Data = null
                };
            }

            if (!string.IsNullOrEmpty(existingCard.CardName))
            {
                existingCard.CardName = card.CardName;
            }

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return new ApiResponse<Card>
                {
                    Success = true,
                    Message = "Card updated successfully.",
                    Data = existingCard
                };
            }

            return new ApiResponse<Card>
            {
                Success = false,
                Message = "An error occurred while updating the card.",
                Data = null
            };
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var success = await _cardRepository.Delete(id);
            if (!success)
            {
                return NotFound(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Card not found.",
                    Data = false
                });
            }

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                Message = "Card deleted successfully.",
                Data = true
            });
        }
    }
}
