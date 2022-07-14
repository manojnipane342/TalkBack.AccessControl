using Microsoft.AspNetCore.Mvc;
using TalkBack.Contacts.Data.Models;

namespace TalkBack.Contacts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsRepository _repo;

        public ContactsController(IContactsRepository repo)
        {
            _repo = repo;
        }

        [HttpGet()]
        public IEnumerable<Contact> GetAllRegistred()
        {
            return _repo.GetAllRegistredUsers();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Contact>> Get(Guid id)
        {
            var contact = await _repo.Get(id);
            if (contact == null)
                return NotFound($"Id: '{id}' not exist");
            return Ok(contact);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Contact>> Delete(Guid id)
        {
            var contact = await _repo.Delete(id);
            if (contact == null)
                return NotFound($"Id: '{id}' not exist");
            return Ok(contact);

            /// todo: notify reqistred services
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Contact contact)
        {
            await _repo.Add(contact);

            if (contact == null)
            {
                return BadRequest();
            }
            return Ok(contact);

            /// todo: notify reqistred services

        }

        [HttpGet("updateStatus")]
        public async Task<IActionResult> UpdateStatus(Guid userid, bool status)
        {
            var result = await _repo.UpdateStatus(userid, status);
            return Ok(result);
        }
    }
}