using back.Dtos;
using back.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace back.Services
{
    public class AuthService : IAuthService
    {
     
        private readonly string _secretKey = "this is my custom Secret key for authentication";
        private readonly string _issuer = "https://localhost:7137";
        private readonly string _audience = "http://localhost:4200";
        private readonly double _expirationMinutes = 360000;

        private readonly IUsersService _usersService;

        public AuthService(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public async Task<LoggedInUserDTO> Login(LoginDTO loginDTO)
        {
            var user = await this._usersService.FindUserByEmailAsync(loginDTO.Email);
            var password = user.Password;
           
            if (password == null || !password.Equals(loginDTO.Password)) {
                throw new Exception("Wrong password");
            };
            var jwtToken = this.GenerateJwtToken(user.Id.ToString(), user.Role);
            return new LoggedInUserDTO(jwtToken, user);
        }

        public async Task<User> Register(User newUser)
        {
            newUser.Role = "PATIENT";
            var allUsers = await this._usersService.GetUsersAsync();
            foreach(User u in allUsers)
            {
                if(u.Role == "DOCTOR")
                {
                    newUser.AssignedGeneralPracticeDoctor = u;
                    newUser.AssignedGeneralPracticeDoctorId = u.Id;
                }
            }
            var user = await this._usersService.CreateUserAsync(newUser);

            return user;
        }

        private string GenerateJwtToken(string userId, string userRole)
        {
            Console.WriteLine(userId);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim("UserRole", userRole)

            }),
                Expires = DateTime.UtcNow.AddMinutes(_expirationMinutes),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
