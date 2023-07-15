using back.Models;

namespace back.Dtos
{
    public class LoggedInUserDTO
    {
        public string Token { get; set; }
        public User User { get; set; }

        public LoggedInUserDTO(string token, User user) {
            this.Token = token;
            this.User = user;
        }
    }
}
