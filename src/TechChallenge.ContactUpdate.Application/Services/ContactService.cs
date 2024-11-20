using TechChallenge.ContactUpdate.Application.DTO;
using TechChallenge.ContactUpdate.Application.Mapping;
using TechChallenge.Domain.Contracts;
using TechChallenge.Domain.Repository;
using TechChallenge.Domain.Shared;
using Flunt.Notifications;
using System.Net;

namespace TechChallenge.ContactUpdate.Application.Services;
public class ContactService : Notifiable<Notification>, IContactService
{
  private readonly IContactRepository _repository;

  public ContactService(IContactRepository repository)
  {
    _repository = repository;
  }

  public async Task<IResponse> UpdateContact(ContactUpdateDTO contactDto)
  {
    try
    {
      contactDto.Validate();

      if (!contactDto.IsValid)
      {
        return new BaseResponse(HttpStatusCode.BadRequest, false, contactDto.Notifications);
      }

      var contact = ContactMapping.FromUpdateDTO(contactDto);
      var contactResponse = await _repository.UpdateContact(contact);

      if (String.IsNullOrWhiteSpace(contactResponse.Name))
      {
        return new BaseResponse(HttpStatusCode.NotFound, false, new List<Notification>() { new Notification("UpdateContact", "Contato não encontrado") });
      }

      var contactResponseDto = ContactMapping.ToResponseDTO(contactResponse);

      return new BaseResponse(HttpStatusCode.OK, true, "Atualização do contato realizada.", contactResponseDto);

    }
    catch
    {
      return new BaseResponse(HttpStatusCode.InternalServerError, false, new List<Notification>() { new Notification("UpdateContact", "Internal Server Error") });
    }   
  }
}
