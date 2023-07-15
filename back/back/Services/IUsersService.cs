using back.Models;
using Microsoft.EntityFrameworkCore;

namespace back.Services
{
    public interface IUsersService
    {
        Task<List<User>> GetUsersAsync();

        Task<User> GetUserByIdAsync(long id);

        Task<User> UpdateUserAsync(long id, User user);

        Task<User> CreateUserAsync(User user);

        Task<bool> DeleteUserAsync(long id);

        Task<User> FindUserByEmailAsync(string email);

        Task<bool> IsFemaleAsync(long id);

        Task<User> BlockUserAsync(long id);

        Task<User> UnblockUserAsync(long id);
    }
}
