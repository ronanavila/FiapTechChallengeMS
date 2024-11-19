using Flunt.Notifications;
using Flunt.Validations;
using System.Text.RegularExpressions;

namespace TechChallenge.Application.DTO;
public class ContactUpdateDTO : Notifiable<Notification>
{
  public Guid Guid { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }
  public int DDD { get; set; }
  public string Phone { get; set; }
  public ContactUpdateDTO(Guid guid, string name, string email, int dDD, string phone)
  {
    Guid = guid;
    Name = name;
    Email = email;
    DDD = dDD;
    Phone = phone;
  }

  public void Validate()
  {
    AddNotifications(
         new Contract<ContactCreationDTO>()
            .Requires()
            .IsGreaterThan(Name, 2, "Name", "Nome deve ter no minimo 3 caracteres.")
            .IsEmail(Email, "Email", "E-mail inválido.")
            .IsGreaterThan(Phone, 7, "Phone", "Telefone tem que ter no minimo 8 números.")
            .IsLowerThan(Phone, 10, "Phone", "Telefone tem que ter no máximo 9 números.")
            .IsGreaterThan(DDD, 10, "DDD está abaixo da faixa no Brasil.")
            .IsLowerThan(DDD, 100, "DDD está acima da faixa no Brasil.")
            .IsGreaterThan(Guid.ToString(), 35, "Guid tem que ter 36 caracteres.")
            .IsLowerThan(Guid.ToString(), 37, "Guid tem que ter 36 caracteres.")

    );

    if (!Regex.IsMatch(Phone.ToString(), @"^\d+$"))
      AddNotification("Phone", "Informe somente números no telefone");
  }

}


