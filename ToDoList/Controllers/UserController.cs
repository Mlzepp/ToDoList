using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoList.Data.Repositories;
using ToDoList.Model;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
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
    }
}
