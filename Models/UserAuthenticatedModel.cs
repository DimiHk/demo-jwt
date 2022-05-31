namespace demo_jwt.Models
{
    public class UserAuthenticatedModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Token { get; set; }
    }
}