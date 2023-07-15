using back.Dtos;
using back.Models;
using back.Repositories;

namespace back.Services
{
    public class AuthService : IAuthService
    {
        

        public Task<LoggedInUserDTO> Login(LoginDTO loginDTO)
        {
            throw new NotImplementedException();
        }

        public Task<User> Register(User newUser)
        {
            throw new NotImplementedException();
        }
    }
}
