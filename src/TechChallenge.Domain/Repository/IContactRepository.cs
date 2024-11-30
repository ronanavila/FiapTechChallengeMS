using System;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Domain.Repository;
public interface IContactRepository : IRepository<Contact>
{
  Task UpdateContact(Contact contact);
  Task<List<Contact>> GetContactByRegion(int ddd);
  Task<List<Contact>> GetAllContacts();
  Task CreateContact(Contact contact);
  Task DeleteContact(Guid guid);
  Task<Contact?> GetContactById(Guid guid);


}
