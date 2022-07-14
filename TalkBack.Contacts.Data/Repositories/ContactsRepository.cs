using TalkBack.Contacts.Data.Context;
using TalkBack.Contacts.Data.Models;

namespace TalkBack.Contacts.Data.Repositories
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly TalkBackContactsDbContext _context;

        public ContactsRepository(TalkBackContactsDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Add(Contact contact)
        {

            contact.Id = Guid.NewGuid();

            _context.Contacts.Add(contact);
             await _context.SaveChangesAsync();
            return  contact.Id;
        }

        public async Task<Contact> Delete(Guid id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact != null)
                _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return contact!;
        }

        public IQueryable<Contact> GetAllRegistredUsers()
        {
            return _context.Contacts;
        }

        public async Task<Contact> Get(Guid id)
        {
            var contact = await _context.Contacts!.FindAsync(id)!;
            if (contact != null)
                return contact;
            else return null!;
        }

        public async Task<bool> UpdateStatus(Guid userid, bool status)
        {
            var _contact = _context.Contacts.FirstOrDefault(x => x.UserId == userid);
            if (_contact != null)
            {
                _contact.Status = status;

               return await _context.SaveChangesAsync() > 0;
            }
            return false!;
        }
    }
}
