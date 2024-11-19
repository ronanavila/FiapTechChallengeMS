using TechChallenge.Domain.Contracts;

namespace TechChallenge.ContactDelete.Application.Services;
public interface IContactService
{
  Task<IResponse> Delete(Guid guid);
}
