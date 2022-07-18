using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoList.Data.Repositories;
using ToDoList.Model;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ListController : Controller
    {
        private readonly IListRepository _listRepository;

        public ListController(IListRepository listRepository)
        {
            _listRepository = listRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            return Ok(await _listRepository.GetAllItems());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemsDetails(int id)
        {
            return Ok(await _listRepository.GetItemDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> InsertItem([FromBody] AList list)
        {
            if (list == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _listRepository.InsertItem(list);

            return Created("created", created);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateItem([FromBody] AList list)
        {
            if (list == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _listRepository.UpdateItem(list);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {

            await _listRepository.DeleteItem(new AList { Id = id });

            return NoContent();
        }
    }
}
