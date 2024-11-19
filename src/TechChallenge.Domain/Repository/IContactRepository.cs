using TechChallenge.Domain.Entities;

namespace TechChallenge.Domain.Repository;
public interface IContactRepository : IRepository<Contact>
{
  Task<Contact> UpdateContact(Contact contact);
  Task<List<Contact>> GetContactByRegion(int ddd);
  Task<List<Contact>> GetAllContacts();
  Task<Guid> CreateContact(Contact contact);
}
