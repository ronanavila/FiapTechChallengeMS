using TechChallenge.Application.DTO;
using TechChallenge.Domain.Contracts;

namespace TechChallenge.Application.Services;
public interface IContactService
{
  Task<IResponse> CreateContact(ContactCreationDTO contactDto);
  Task<IResponse> GetAllContacts();
  Task<IResponse> Delete(Guid guid);
  Task<IResponse> UpdateContact(ContactUpdateDTO contactDto);
  Task<IResponse> GetContactByRegion(int ddd);
}
