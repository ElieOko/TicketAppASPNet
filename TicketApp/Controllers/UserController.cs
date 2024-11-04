using Microsoft.AspNetCore.Mvc;
using TicketApp.Interfaces;
using TicketApp.Models;

namespace TicketApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUser _user;
        public UserController(IUser user) 
        {
            _user = user;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetAll()
        {
            var users = _user.GetAll();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(users);
        }
    }
}
