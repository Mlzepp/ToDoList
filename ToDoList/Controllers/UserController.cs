using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Data.Repositories;
using ToDoList.Model;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        public static User user = new User();
        private readonly string secreyKey;
        private readonly IUserRepository _userRepository;

        public UserController(IConfiguration config, IUserRepository userRepository)
        {
            secreyKey = config.GetSection("Settings").GetSection("Secretkey").ToString();
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _userRepository.RegisterUser(user);

            if (created == true)
            { 
                return Created("created", created);
            }
            else
            {
                return BadRequest("User already registered");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] User request)
        {
            if (request == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
           
            var result = await _userRepository.LoginUser(request);
            
            if (result == true)
            {
                string token = CreateToken(request);

                return Ok(token);
            }
            else
            {
                return BadRequest("Wrong password.");
            }
        }

        private string CreateToken(User user)
        {
            var keyBytes = Encoding.ASCII.GetBytes(secreyKey);
            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Username));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokenCreated = tokenHandler.WriteToken(tokenConfig);

            return tokenCreated;
        }
    }
}
