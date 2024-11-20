using TechChallenge.Domain.Contracts;

namespace TechChallenge.ContactSearch.Application.Services;
public interface IContactService
{
  Task<IResponse> GetAllContacts();
  Task<IResponse> GetContactByRegion(int ddd);
}
