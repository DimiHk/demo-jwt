using demo_jwt.Entities;
using demo_jwt.Interfaces;
using demo_jwt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace demo_jwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("authenticateUser")]
        public UserAuthenticatedModel AuthenticateUser(UserLoginModel userLoginModel)
        {
            return _service.AuthenticateUser(userLoginModel);
        }

        [Authorize]
        [HttpGet]
        [Route("getAllUsers")]
        public IEnumerable<User> GetUsers() => _service.GetUsers();
    }
}