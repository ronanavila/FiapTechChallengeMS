using Flunt.Notifications;
using Flunt.Validations;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Text.RegularExpressions;

namespace TechChallenge.Application.DTO;
public class ContactCreationDTO : Notifiable<Notification>
{
  public string Name { get; set; }
  public string Email { get; set; }
  public int DDD { get; set; }
  public string Phone { get; set; }
  public ContactCreationDTO(string name, string email, int ddd, string phone)
  {
    Name = name;
    Email = email;
    DDD = ddd;
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

    );

    if (!Regex.IsMatch(Phone.ToString(), @"^\d+$"))
      AddNotification("Phone", "Informe somente números no telefone");
  }
}


