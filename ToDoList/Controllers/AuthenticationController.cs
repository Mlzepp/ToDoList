using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using ToDoList.Data.Repositories;
using ToDoList.Model;
using Microsoft.Extensions.Configuration;
using System.Text;
using System;

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
