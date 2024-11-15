using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static TicketApp.Controllers.BranchController;
using TicketApp.Data;
using TicketApp.Interfaces;
using TicketApp.Models;
using Microsoft.EntityFrameworkCore;

namespace TicketApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ICustomer _customerRepository;

        public CustomerController(DataContext context, ICustomer customerRepository)
        {
            _context = context;
            _customerRepository = customerRepository;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<Customer>>>> GetCustomers()
        {
            var customers = await _customerRepository.GetAll();
            var response = new ApiResponse<IEnumerable<Customer>>
            {
                Success = true,
                Message = "Customers retrieved successfully.",
                Data = customers
            };

            return Ok(response);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Customer>>> GetCustomer(int id)
        {
           
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null)
            {
                return NotFound(new ApiResponse<Customer>
                {
                    Success = false,
                    Message = "customer not found",
                    Data = null
                });
            }

            var response = new ApiResponse<Customer>
            {
                Success = true,
                Message = "customer created successfully.",
                Data = customer
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ApiResponse<Customer>> Store(Customer customer)
        {

            if (
                string.IsNullOrWhiteSpace(customer.FirstName) ||string.IsNullOrWhiteSpace(customer.lastName)||
                string.IsNullOrWhiteSpace(customer.fatherName)|| string.IsNullOrWhiteSpace(customer.phoneNumber1)||
                string.IsNullOrWhiteSpace(customer.motherName) || string.IsNullOrWhiteSpace(customer.whatsappNumber)

                )
                return new ApiResponse<Customer>
                {
                    Success = false,
                    Message = "FirstName,lastName,fatherName,motherName and whatsappNumber  are required.",
                    Data = null,

                };

            if (await _context.Customers.AnyAsync(b => b.whatsappNumber == customer.whatsappNumber))
            {
                return new ApiResponse<Customer>
                {
                    Success = false,
                    Message = "This customer already exists.",
                    Data = null
                };
            }

            _context.Customers.Add(customer);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return new ApiResponse<Customer>
                {
                    Success = true,
                    Message = "customer added successfully.",
                    Data = customer
                };
            }

            return new ApiResponse<Customer>
            {
                Success = false,
                Message = "An error occurred while adding the Customer.",
                Data = null
            };
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse<Customer>> Update(int id, Customer customer)
        {
            ModelState.Clear();
            var existingCustomer = await _context.Customers.FindAsync(id);

            if (existingCustomer == null)
            {
                return new ApiResponse<Customer>
                {
                    Success = false,
                    Message = "Customer not found.",
                    Data = null
                };
            }

            if (!string.IsNullOrEmpty(customer.FirstName))
            {
                existingCustomer.FirstName = customer.FirstName;
            }

            if (!string.IsNullOrEmpty(customer.lastName))
            {
                existingCustomer.lastName = customer.lastName;
            }
            if (!string.IsNullOrEmpty(customer.fatherName))
            {
                existingCustomer.fatherName = customer.fatherName;
            }
            if (!string.IsNullOrEmpty(customer.motherName))
            {
                existingCustomer.motherName = customer.motherName;
            }
            if (!string.IsNullOrEmpty(customer.phoneNumber1))
            {
                existingCustomer.phoneNumber1 = customer.phoneNumber1;
            }
            if (!string.IsNullOrEmpty(customer.whatsappNumber))
            {
                existingCustomer.whatsappNumber = customer.whatsappNumber;
            }
            if (!string.IsNullOrEmpty(customer.phoneNumber1))
            {
                existingCustomer.phoneNumber1 = customer.phoneNumber1;
            }

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return new ApiResponse<Customer>
                {
                    Success = true,
                    Message = "Customer updated successfully.",
                    Data = existingCustomer
                };
            }

            return new ApiResponse<Customer>
            {
                Success = false,
                Message = "An error occurred while updating the Customer.",
                Data = null
            };
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var success = await _customerRepository.Delete(id);
            if (!success)
            {
                return NotFound(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Customer not found.",
                    Data = false
                });
            }

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                Message = "Customer deleted successfully.",
                Data = true
            });
        }
    }
}
