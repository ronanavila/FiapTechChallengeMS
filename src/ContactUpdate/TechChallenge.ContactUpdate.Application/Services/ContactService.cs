using Flunt.Notifications;
using System.Net;
using TechChallenge.ContactUpdate.Application.DTO;
using TechChallenge.ContactUpdate.Application.Mapping;
using TechChallenge.Domain.Contracts;
using TechChallenge.Domain.Shared;

namespace TechChallenge.ContactUpdate.Application.Services;
public class ContactService : Notifiable<Notification>, IContactService
{

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
 
      return new BaseResponse(HttpStatusCode.Accepted,  true, "Atualização do contato realizada.", contact);

    }
    catch
    {
      return new BaseResponse(HttpStatusCode.InternalServerError, false, new List<Notification>() { new Notification("UpdateContact", "Internal Server Error") });
    }   
  }
}
