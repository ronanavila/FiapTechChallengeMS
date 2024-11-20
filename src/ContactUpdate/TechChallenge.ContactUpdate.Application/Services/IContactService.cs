using TechChallenge.ContactUpdate.Application.DTO;
using TechChallenge.Domain.Contracts;

namespace TechChallenge.ContactUpdate.Application.Services;
public interface IContactService
{
  Task<IResponse> UpdateContact(ContactUpdateDTO contactDto);
}
