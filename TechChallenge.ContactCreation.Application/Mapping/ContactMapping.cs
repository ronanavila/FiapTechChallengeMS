using TechChallenge.ContactCreation.Application.DTO;
using TechChallenge.Domain.Entities;

namespace TechChallenge.ContactCreation.Application.Mapping;
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
}
