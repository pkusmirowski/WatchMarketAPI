using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WatchMarketAPI.DTOs;
using WatchMarketAPI.Interfaces;
using WatchMarketAPI.Models;

namespace WatchMarketAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IWatchesContext _context;

        public UserService(IWatchesContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterUser(CreateUserDto userDto)
        {

            try
            {
                // Walidacja danych wejściowych
                if (string.IsNullOrEmpty(userDto.Username) || string.IsNullOrEmpty(userDto.Email) || string.IsNullOrEmpty(userDto.Password))
                {
                    throw new ArgumentException("Username, email, and password must not be empty");
                }

                // Sprawdzenie, czy użytkownik o podanym adresie email już istnieje w bazie danych
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == userDto.Email);
                if (existingUser != null)
                {
                    throw new InvalidOperationException("User with this email already exists");
                }

                string passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(userDto.Password, 13);

                var user = new User
                {
                    Username = userDto.Username,
                    Email = userDto.Email,
                    PasswordHash = passwordHash,
                    Role = "user"
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating user: Database update failed", ex);
            }
        }

        public async Task<string> LoginUser(string Email, string Password)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                throw new ArgumentException("Email and password must not be empty");
            }

            var selectUser = await _context.Users.FirstOrDefaultAsync(user => user.Email == Email);

            if(selectUser != null)
            {
                bool passwordCheck = BCrypt.Net.BCrypt.EnhancedVerify(Password, selectUser.PasswordHash);

                if(passwordCheck)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes("TestKey");

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, selectUser.Id.ToString())
                        }),
                        Expires = DateTime.UtcNow.AddHours(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    return tokenHandler.WriteToken(token);
                }
            }

            return null;
        }
    }
}
