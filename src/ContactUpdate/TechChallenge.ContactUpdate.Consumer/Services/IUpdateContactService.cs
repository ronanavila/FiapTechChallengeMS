using TechChallenge.Domain.Entities;

namespace TechChallenge.ContactUpdate.Consumer.Services;
public interface IUpdateContactService
{
  Task UpdateContact(Contact contact);
}
