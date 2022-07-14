using TalkBack.Contacts.Data.Models;

namespace TalkBack.Contacts.Data.Repositories
{
    public interface IContactsRepository
    {
        IQueryable<Contact> GetAllRegistredUsers();

        Task<Contact> Get(Guid id);

        Task<Guid> Add(Contact user);

        Task<Contact> Delete(Guid id);

        Task<bool> UpdateStatus(Guid userid, bool status);

    }
}
