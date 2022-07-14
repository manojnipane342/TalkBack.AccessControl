using TalkBackAccessControl.Data.Models;

namespace TalkBackAccessControl.Data.Repositories
{
    public interface IUsersRepository
    {
        Task<Guid> Add(User user);
        Task<User> Login(DtoUser userLogin);
        Task<bool> UserExists(string username);
        IQueryable<User> GetAll();

        Task<User> Get(Guid id);

        Task<User> Get(string username);

        Task<User> Update(DtoUser user, Guid id);
        Task<User> Delete(Guid id);

    }
}
