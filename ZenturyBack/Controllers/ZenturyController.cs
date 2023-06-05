using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ZenturyBack.BO;
using ZenturyBack.Models;

namespace ZenturyBack.Controllers
{
    [Route("zentury")]
    [ApiController]
    public class ZenturyController : ControllerBase
    {
        private readonly DataService _dataService;
        private IConfiguration _config;


        public ZenturyController(DataService dataService, IConfiguration config)
        {
            _dataService = dataService;
            _config = config;
        }

        [Authorize]
        [HttpGet("users")]
        public async Task<ActionResult<UsersPaginated>> GetAllUsers([FromQuery] UsersResource usersResource)
        {
            return await _dataService.GetAllUsers(usersResource);
        }

        [Authorize]
        [HttpGet("logins")]
        public async Task<ActionResult<LoginsPaginated>> GetAllLogins([FromQuery] LoginResource loginResource)
        {
            return await _dataService.GetAllLogins(loginResource);
        }

        [Authorize]
        [HttpPost("adduser")]
        public async Task<ActionResult<User>> SaveUser(User user)
        {
            return await _dataService.AddUser(user);
        }

        [AllowAnonymous]
        [HttpPost("/signin")]
        public IActionResult SignIn([FromBody] Signin signin)
        {
            IActionResult response = Unauthorized();
            var user = _dataService.AuthenticateUser(signin);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Key"],
              null,
              expires: DateTime.Now.AddMinutes(1440),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [AllowAnonymous]
        [HttpGet("isAuth")]
        public bool IsAuthenticated()
        {
            return User.Identity.IsAuthenticated;
        }

    }

}
