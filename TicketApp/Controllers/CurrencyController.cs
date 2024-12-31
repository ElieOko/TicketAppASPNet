using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static TicketApp.Controllers.BranchController;
using TicketApp.Repository;
using TicketApp.Data;
using TicketApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TicketApp.Models;

namespace TicketApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ICurrency _currencyRepository;

        public CurrencyController(DataContext context, ICurrency currencyRepository)
        {
            _context = context;
            _currencyRepository = currencyRepository;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<Currency>>>> GetCurrencies()
        {
            var currencies = await _currencyRepository.GetAll();
            var response = new ApiResponse<IEnumerable<Currency>>
            {
                Success = true,
                Message = "Currencies retrieved successfully.",
                Data = currencies
            };

            return Ok(response);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Currency>>> GetCurrency(int id)
        {
            var currency = await _context.Currencies
                .Include(b => b.tickets)
                .Include(b => b.intervals)
                .FirstOrDefaultAsync(b => b.CurrencyId == id);

            if (currency == null)
            {
                return NotFound(new ApiResponse<Currency>
                {
                    Success = false,
                    Message = "currency not found",
                    Data = null
                });
            }

            var response = new ApiResponse<Currency>
            {
                Success = true,
                Message = "Currency created successfully.",
                Data = currency
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ApiResponse<Currency>> Store(Currency currency)
        {

            if (string.IsNullOrWhiteSpace(currency.CurrencyCode) || string.IsNullOrWhiteSpace(currency.CurrencyName))
                return new ApiResponse<Currency>
                {
                    Success = false,
                    Message = "CurrencyCode and CurrencyCode are required.",
                    Data = null,

                };

            if (await _context.Currencies.AnyAsync(b => b.CurrencyName == currency.CurrencyName))
            {
                return new ApiResponse<Currency>
                {
                    Success = false,
                    Message = "A currency with this name already exists.",
                    Data = null
                };
            }

            _context.Currencies.Add(currency);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return new ApiResponse<Currency>
                {
                    Success = true,
                    Message = "Currency added successfully.",
                    Data = currency
                };
            }

            return new ApiResponse<Currency>
            {
                Success = false,
                Message = "An error occurred while adding the currency.",
                Data = null
            };
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse<Currency>> Update(int id, Currency currency)
        {
            ModelState.Clear();
            var existingCurrency = await _context.Currencies.FindAsync(id);

            if (existingCurrency == null)
            {
                return new ApiResponse<Currency>
                {
                    Success = false,
                    Message = "Currency not found.",
                    Data = null
                };
            }

            if (!string.IsNullOrEmpty(currency.CurrencyName))
            {
                existingCurrency.CurrencyName = currency.CurrencyName;
            }

            if (!string.IsNullOrEmpty(currency.CurrencyCode))
            {
                existingCurrency.CurrencyCode = currency.CurrencyCode;
            }

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return new ApiResponse<Currency>
                {
                    Success = true,
                    Message = "Currency updated successfully.",
                    Data = existingCurrency
                };
            }

            return new ApiResponse<Currency>
            {
                Success = false,
                Message = "An error occurred while updating the currency.",
                Data = null
            };
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var success = await _currencyRepository.Delete(id);
            if (!success)
            {
                return NotFound(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Currency not found.",
                    Data = false
                });
            }

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                Message = "Currency deleted successfully.",
                Data = true
            });
        }

    }
}
