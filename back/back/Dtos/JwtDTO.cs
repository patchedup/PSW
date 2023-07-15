namespace back.Dtos
{
    public class JwtDTO
    {
        public string Token { get; set; }

        public JwtDTO(string token) {
            this.Token = token;
        }
    }
}
