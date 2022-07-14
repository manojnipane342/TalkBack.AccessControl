using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using TalkBackAccessControl.Data.Models;

namespace Api_TalkBack.AccessControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsersRepository _repo;
        private readonly IContactApi _contactApi;

        public LoginController(IUsersRepository repo, IContactApi contactApi)
        {
            _repo = repo;
            _contactApi = contactApi;
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(DtoUser userLogin)
        {
            try
            {

                var user = await _repo.Login(userLogin);

                if (user == null) return Unauthorized("Invalid UserName");

                var hmac = new HMACSHA512(user.PasswordSalt!);

                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userLogin.Password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != user.PasswordHash![i]) return Unauthorized("Invalid Password");
                }

                _contactApi.ApiConnection(user);


                return user;

            }
            catch (Exception)
            {
                return Unauthorized();
            }

        }
    }
}
