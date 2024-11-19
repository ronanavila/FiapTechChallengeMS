using TechChallenge.ContactDelete.Application.DTO;
using TechChallenge.Domain.Entities;

namespace TechChallenge.ContactDelete.Application.Mapping;
public static class ContactMapping
{
  public static ContactResponseDTO ToResponseDTO(this Contact contact)
  {
    ContactResponseDTO contactDto = new(contact.Guid, contact.Name, contact.Email, contact.RegionDDD, contact.Phone, contact.Region);
    return contactDto;
  }
}
