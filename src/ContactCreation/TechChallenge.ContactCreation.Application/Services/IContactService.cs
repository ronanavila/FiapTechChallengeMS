using TechChallenge.ContactCreation.Application.DTO;
using TechChallenge.Domain.Contracts;

namespace TechChallenge.ContactCreation.Application.Services;
public interface IContactService
{
  Task<IResponse> CreateContactValidation(ContactCreationDTO contactDto);

}
