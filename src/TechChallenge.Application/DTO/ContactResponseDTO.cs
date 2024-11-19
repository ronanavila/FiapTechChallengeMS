using TechChallenge.Domain.Entities;

namespace TechChallenge.Application.DTO;
public record ContactResponseDTO

{
  public ContactResponseDTO()
  {

  }
  public ContactResponseDTO(Guid guid, string name, string email, int dDD, string phone, Region region)
  {
    Guid = guid;
    Name = name;
    Email = email;
    DDD = dDD;
    Phone = phone;
    Region = region;
  }

  public Guid Guid { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }
  public int DDD { get; set; }
  public string Phone { get; set; }
  public Region Region { get; set; }


}