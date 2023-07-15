using back.Dtos;
using back.Models;

namespace back.Services
{
    public interface IAuthService
    {

        Task<LoggedInUserDTO> Login(LoginDTO loginDTO);

        Task<User> Register(User newUser);
    }
}
