using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Repository;

namespace TechChallenge.ContactUpdate.Consumer.Services;
public class UpdateContactService : IUpdateContactService
{
  private readonly IContactRepository _repository;

  public UpdateContactService(IContactRepository repository)
  {
    _repository = repository;
  }


  public async Task UpdateContact(Contact? contact)
  {
    if (contact is not null)
    {
      await _repository.UpdateContact(contact);
    }
  }
}
