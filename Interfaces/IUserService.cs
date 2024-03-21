using System.Threading.Tasks;
using WatchMarketAPI.DTOs;
using WatchMarketAPI.Models;

namespace WatchMarketAPI.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterUser(CreateUserDto userDto);
        Task<string> LoginUser(string email, string password);
    }
}