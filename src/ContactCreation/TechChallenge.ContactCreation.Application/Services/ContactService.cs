using Flunt.Notifications;
using MassTransit;
using Microsoft.Extensions.Configuration;
using System.Net;
using TechChallenge.ContactCreation.Application.DTO;
using TechChallenge.ContactCreation.Application.Mapping;
using TechChallenge.Domain.Contracts;
using TechChallenge.Domain.Shared;
using static MassTransit.MessageHeaders;

namespace TechChallenge.ContactCreation.Application.Services;
public class ContactService : Notifiable<Notification>, IContactService
{


  public async Task<IResponse> CreateContactValidation(ContactCreationDTO contactDto)
  {
    try
    {
      contactDto.Validate();

      if (!contactDto.IsValid)
      {
        return new BaseResponse(HttpStatusCode.BadRequest, false, contactDto.Notifications);
      }

      var contact = ContactMapping.FromCreationDTO(contactDto);

      return new BaseResponse(HttpStatusCode.Accepted, true, "Cadastro Enviado.", contact);
    }
    catch
    {
      return new BaseResponse(HttpStatusCode.InternalServerError,false, new List<Notification>() { new Notification("CreateContact", "Internal Server Error") });
    }
  }
}
