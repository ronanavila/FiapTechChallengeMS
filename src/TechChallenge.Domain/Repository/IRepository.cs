using TechChallenge.Domain.Entities;

namespace TechChallenge.Domain.Repository;
public interface IRepository<T> where T : BaseEntity
{
  Task<IList<T>> GetAll();
  Task<T> GetById(Guid guid);
  Task<T> Create(T entidade);
  Task<T> Update(T entidade);
  Task<T> Delete(Guid guid);
}
