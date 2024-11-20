using Flunt.Notifications;
using System.Net;
using TechChallenge.ContactDelete.Application.Mapping;
using TechChallenge.Domain.Contracts;
using TechChallenge.Domain.Repository;
using TechChallenge.Domain.Shared;

namespace TechChallenge.ContactDelete.Application.Services;
public class ContactService : Notifiable<Notification>, IContactService
{
  private readonly IContactRepository _repository;

  public ContactService(IContactRepository repository)
  {
    _repository = repository;
  }

  
  public async Task<IResponse> Delete(Guid guid)
  {
    try
    {
      var contact = await _repository.Delete(guid);

      if (contact is null)
      {
        return new BaseResponse(HttpStatusCode.NotFound, false, new List<Notification>() { new Notification("DeleteContacts", "Nenhum Contato encontrado") });
      }

      return new BaseResponse(HttpStatusCode.OK, true, "Registro removido com sucesso.", ContactMapping.ToResponseDTO(contact));
    }
    catch
    {
      return new BaseResponse(HttpStatusCode.InternalServerError, false, new List<Notification>() { new Notification("DeleteContact", "Internal Server Error") });
    }

  }
}
