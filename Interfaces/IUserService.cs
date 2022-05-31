using demo_jwt.Entities;
using demo_jwt.Models;

namespace demo_jwt.Interfaces
{
    public interface IUserService
    {
        UserAuthenticatedModel AuthenticateUser(UserLoginModel userLoginModel);

        IEnumerable<User> GetUsers();
    }
}