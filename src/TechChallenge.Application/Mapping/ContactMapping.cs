using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using TechChallenge.Application.DTO;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Application.Mapping;
public static class ContactMapping
{
  public static ContactResponseDTO ToResponseDTO(this Contact contact)
  {
    ContactResponseDTO contactDto = new(contact.Guid, contact.Name, contact.Email, contact.RegionDDD, contact.Phone, contact.Region);
    return contactDto;
  }

  public static Contact FromCreationDTO(this ContactCreationDTO contactDto)
  {

    Contact contact = new()
    {
      Name = contactDto.Name,
      Email = contactDto.Email,
      RegionDDD = contactDto.DDD,
      Phone = contactDto.Phone
    };

    return contact;
  }

  public static Contact FromUpdateDTO(this ContactUpdateDTO contactUpdateDto)
  {

    Contact contact = new()
    {
      Guid = contactUpdateDto.Guid,
      Name = contactUpdateDto.Name,
      Email = contactUpdateDto.Email,
      RegionDDD = contactUpdateDto.DDD,
      Phone = contactUpdateDto.Phone
    };

    return contact;
  }
}
