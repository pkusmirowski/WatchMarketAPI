using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WatchMarketAPI.DTOs;
using WatchMarketAPI.Interfaces;
using WatchMarketAPI.Models;

namespace WatchMarketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IWatchesContext _context;
        private readonly IUserService _userService;

        public UserController(IWatchesContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.GetUsersAsync();
        }

        [HttpPost]
        public async Task<ActionResult<User>> RegisterUser(CreateUserDto userDto)
        {
            var registeredUser = await _userService.RegisterUser(userDto);
            return CreatedAtAction("GetUser", new { id = registeredUser.Id }, registeredUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDto model)
        {
            try
            {
                var token = await _userService.LoginUser(model.Email, model.Password);
                if (token != null)
                {
                    return Ok(new { Token = token });
                }
                else
                {
                    return Unauthorized("Invalid email or password");
                }
            }
            catch
            {
                return BadRequest("An error occurred while processing your request.");
            }
        }
    }
}
