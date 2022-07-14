using Microsoft.AspNetCore.Mvc;
using TalkBackAccessControl.Data.Models;

namespace TalkBack.AccessControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _repo;

        public UsersController(IUsersRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _repo.GetAll();
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<User>> Get(Guid id)
        {
            var user = await _repo.Get(id);
            if (user == null)
                return NotFound($"Id: '{id}' not exist");
            return Ok(user);
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<User>> Get(string userName)
        {
            var user = await _repo.Get(userName);
            try
            {
                if (user == null)
                    return BadRequest($"The user name '{userName}' not exist");
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok(user);
        }


        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] DtoUser user)
        {
            var _user = await _repo.Update(user, id);

            if (_user != null)
            {
                return Ok(_user);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<User>> Delete(Guid id)
        {
            var _user = await _repo.Delete(id);
            if (_user == null)
                return NotFound($"Id: '{id}' not exist");
            return Ok(_user);

            /// todo: notify reqistred services
        }
    }
}