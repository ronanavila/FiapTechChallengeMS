using TechChallenge.Domain.Entities;

namespace TechChallenge.ContactDelete.Consumer.Services;
public interface IDeleteContactService
{
  Task DeleteContact(Guid guid);
}
