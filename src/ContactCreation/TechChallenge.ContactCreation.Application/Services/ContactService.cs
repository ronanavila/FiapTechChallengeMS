using TechChallenge.ContactCreation.Application.DTO;
using TechChallenge.ContactCreation.Application.Mapping;
using TechChallenge.Domain.Contracts;
using TechChallenge.Domain.Repository;
using TechChallenge.Domain.Shared;
using Flunt.Notifications;
using System.Net;

namespace TechChallenge.ContactCreation.Application.Services;
public class ContactService : Notifiable<Notification>, IContactService
{
  private readonly IContactRepository _repository;

  public ContactService(IContactRepository repository)
  {
    _repository = repository;
  }

  public async Task<IResponse> CreateContact(ContactCreationDTO contactDto)
  {

    try
    {
      contactDto.Validate();

      if (!contactDto.IsValid)
      {
        return new BaseResponse(HttpStatusCode.BadRequest, false, contactDto.Notifications);
      }

      var contact = ContactMapping.FromCreationDTO(contactDto);
      var contactResponse = await _repository.CreateContact(contact);

      return new BaseResponse(HttpStatusCode.Created, true, "Cadastro realizado.", contactResponse);
    }
    catch
    {
      return new BaseResponse(HttpStatusCode.InternalServerError,false, new List<Notification>() { new Notification("CreateContact", "Internal Server Error") });
    }
  }

}
