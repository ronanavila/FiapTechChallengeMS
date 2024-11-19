using TechChallenge.Application.DTO;
using TechChallenge.Application.Mapping;
using TechChallenge.Domain.Contracts;
using TechChallenge.Domain.Repository;
using TechChallenge.Domain.Shared;
using Flunt.Notifications;
using System.Net;

namespace TechChallenge.Application.Services;
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
