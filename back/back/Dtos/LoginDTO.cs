namespace back.Dtos
{
    public class LoginDTO
    {
        private readonly string _email;
        private readonly string _password;
        

        public LoginDTO(string email, string password)
        {
            _email = email;
            _password = password;
        }

        public string Email { get { return _email; } }
        public string Password { get { return _password; } }



    }
}
