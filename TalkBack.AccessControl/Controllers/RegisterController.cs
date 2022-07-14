using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using TalkBackAccessControl.Data.Models;

namespace Api_TalkBack.AccessControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUsersRepository _repo;

        public RegisterController(IUsersRepository repo)
        {
            _repo = repo;
        }
        [HttpPost("signUp")]
        public async Task<ActionResult<User>> SignUp(DtoUser registerDto)
        {
            try
            {

                if (await _repo.UserExists(registerDto.UserName!))
                {
                    return BadRequest("UserName Is Already Taken");
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
            var hmac = new HMACSHA512();
            var user = new User
            {
                UserName = registerDto.UserName!.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password!)),
                PasswordSalt = hmac.Key,

            };
            await _repo.Add(user);

            return user;
        }
    }
}