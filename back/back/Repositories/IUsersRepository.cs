using System.Collections.Generic;
using System.Threading.Tasks;
using back.Models;

namespace back.Repositories
{
    public interface IUsersRepository
    {
        Task<List<User>> GetUsersAsync();
        Task<User?> GetUserByIdAsync(long id);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<User> DeleteUserAsync(User user);
        Task<User?> FindByEmailAsync(string email);
    }
}
