using TechChallenge.ContactSearch.Application.DTO;
using TechChallenge.ContactSearch.Application.Mapping;
using TechChallenge.Domain.Contracts;
using TechChallenge.Domain.Repository;
using TechChallenge.Domain.Shared;
using Flunt.Notifications;
using System.Net;

namespace TechChallenge.ContactSearch.Application.Services;
public class ContactService : Notifiable<Notification>, IContactService
{
  private readonly IContactRepository _repository;

  public ContactService(IContactRepository repository)
  {
    _repository = repository;
  }


  public async Task<IResponse> GetAllContacts()
  {
    try
    {
      var contacts = await _repository.GetAllContacts();

      var contactsDTO = new List<ContactResponseDTO>();

      if (contacts.Count > 0)
      {
        foreach (var contact in contacts)
        {
          contactsDTO.Add(ContactMapping.ToResponseDTO(contact));
        }
        return new BaseResponse(HttpStatusCode.OK, true, "Consulta executada com sucesso.", contactsDTO);

      }
      return new BaseResponse(HttpStatusCode.NotFound, false, new List<Notification>() { new Notification("GetAllContacts", "Nenhum Contato encontrado") });
    }
    catch
    {
      return new BaseResponse(HttpStatusCode.InternalServerError, false, new List<Notification>() { new Notification("GetAllContacts", "Internal Server Error") });
    }
  }
   public async Task<IResponse> GetContactByRegion(int ddd)
  {
    try
    {
      var contactResponseDto = new List<ContactResponseDTO>();

      var contactResponse = await _repository.GetContactByRegion(ddd);

      if (!contactResponse.Any())
      {
        return new BaseResponse(HttpStatusCode.NotFound, false, new List<Notification>() { new Notification("GetContactByRegion", "Contato não encontrados para a região") });
      }

      foreach (var contact in contactResponse)
      {
        contactResponseDto.Add(ContactMapping.ToResponseDTO(contact));
      }

      return new BaseResponse(HttpStatusCode.OK, true, "Consulta executada com sucesso.", contactResponseDto);
    }
    catch
    {
      return new BaseResponse(HttpStatusCode.InternalServerError, false, new List<Notification>() { new Notification("GetContactByRegion", "Internal Server Error") });
    } 

  }
}
