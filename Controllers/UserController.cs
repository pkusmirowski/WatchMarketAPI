using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchMarketAPI.DTOs;
using WatchMarketAPI.Interfaces; // Dodany using
using WatchMarketAPI.Models;
using WatchMarketAPI.Services;

namespace WatchMarketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IWatchesContext _context;
        private readonly IUserService _userService; // Zmiana na interfejs IUserService

        public UserController(IWatchesContext context, IUserService userService) // Zmiana w konstruktorze
        {
            _context = context;
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.GetUsersAsync();
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(CreateUserDto userDto)
        {
            var user = await _userService.RegisterUser(userDto);
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }
    }
}

// GET: api/User/5
//[HttpGet("{id}")]
//public async Task<ActionResult<User>> GetUser(int id)
//{
//    var user = await _context.Users.FindAsync(id);

//    if (user == null)
//    {
//        return NotFound();
//    }

//    return user;
//}

//// PUT: api/User/5
//// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//[HttpPut("{id}")]
//public async Task<IActionResult> PutUser(int id, User user)
//{
//    if (id != user.Id)
//    {
//        return BadRequest();
//    }

//    _context.UpdateEntity(user);

//    try
//    {
//        await _context.SaveChangesAsync();
//    }
//    catch (DbUpdateConcurrencyException)
//    {
//        if (!UserExists(id))
//        {
//            return NotFound();
//        }
//        else
//        {
//            throw;
//        }
//    }

//    return NoContent();
//}

// DELETE: api/User/5
//[HttpDelete("{id}")]
//public async Task<IActionResult> DeleteUser(int id)
//{
//    var user = await _context.Users.FindAsync(id);
//    if (user == null)
//    {
//        return NotFound();
//    }

//    _context.Users.Remove(user);
//    await _context.SaveChangesAsync();

//    return NoContent();
//}

//private bool UserExists(int id)
//{
//    return _context.Users.Any(e => e.Id == id);
//}

