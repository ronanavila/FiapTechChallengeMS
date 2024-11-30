using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Repository;

namespace TechChallenge.ContactCreation.Consumer.Services;
public class CreateContactService : ICreateContactService
{
  private readonly IContactRepository _repository;

  public CreateContactService(IContactRepository repository)
  {
    _repository = repository;
  }


  public async Task CreateContact(Contact? contact)
  {
    if(contact is not null)
    {
    await _repository.CreateContact(contact);
    
    }
  }

}
