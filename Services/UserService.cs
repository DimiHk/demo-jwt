using demo_jwt.Entities;
using demo_jwt.Helpers;
using demo_jwt.Interfaces;
using demo_jwt.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace demo_jwt.Services
{
    public class UserService : IUserService
    {
        private AppSettings _appSettings;

        public UserService(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        private IEnumerable<User> _users = new List<User>()
        {
            new User() {Id = 1, FirstName = "Admin", LastName = "Teste", Username = "Admin", Password = "@Teste123"}
        };

        public UserAuthenticatedModel AuthenticateUser(UserLoginModel userLoginModel)
        {
            var user = _users.FirstOrDefault(x => x.Username == userLoginModel.Username && x.Password == userLoginModel.Password);

            if (user == null) return null;

            return new UserAuthenticatedModel() { Id = user.Id, Username = user.Username, Token = generateJwtToken(user) };

        }

        public IEnumerable<User> GetUsers()
        {
            return _users;
        }

        private string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}