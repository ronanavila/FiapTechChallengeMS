using TechChallenge.Domain.Repository;

namespace TechChallenge.ContactDelete.Consumer.Services;
public class DeleteContactService : IDeleteContactService
{
  private readonly IContactRepository _repository;

  public DeleteContactService(IContactRepository repository)
  {
    _repository = repository;
  }


  public async Task DeleteContact(Guid guid)
  {
    await _repository.DeleteContact(guid);
  }

}
