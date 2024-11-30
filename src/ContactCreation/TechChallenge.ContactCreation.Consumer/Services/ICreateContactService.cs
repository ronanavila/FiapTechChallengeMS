using TechChallenge.Domain.Entities;

namespace TechChallenge.ContactCreation.Consumer.Services;
public interface ICreateContactService
{
  Task CreateContact(Contact contact);
}
