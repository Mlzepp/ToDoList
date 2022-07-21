using Microsoft.AspNetCore.Http;
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
    public class AuthenticationController : ControllerBase
    {
        private readonly string secreyKey;

        public AuthenticationController(IConfiguration config)
        {
            secreyKey = config.GetSection("Settings").GetSection("Secretkey").ToString();
        }

        //Esta parte es la que estaba en UserController
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _userRepository.CreateUser(user);

            return Created("created", created);
        }

        [HttpGet("{username},{password}")]
        public async Task<IActionResult> LoguinUser(string username, string password)
        {
            return Ok(await _userRepository.LoguinUser(username, password));
        }

        //Hasta aca...


        [HttpPost]
        [Route("Validate")]
        public IActionResult Validate([FromBody] User request)
        {
            if (request.Username == "matias.losada@live.com" && request.Password == "Marley01")
            {
                var keyBytes = Encoding.ASCII.GetBytes(secreyKey);
                var claims = new ClaimsIdentity();

                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.Username));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddHours(8),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokenCreated = tokenHandler.WriteToken(tokenConfig);

                return StatusCode(StatusCodes.Status200OK, new { token = tokenCreated });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest); 
            }
        }           
    }
}
