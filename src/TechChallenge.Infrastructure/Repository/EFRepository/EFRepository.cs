
using Microsoft.EntityFrameworkCore;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Repository;
using TechChallenge.Infrastructure.Repository.ApplicationDbContext;

namespace Infrastructure.Repository.EFRepository;
public class EFRepository<T> : IRepository<T> where T : BaseEntity
{
  protected ApplicationDbContext _context;
  protected DbSet<T> _dbSet;

  public EFRepository(ApplicationDbContext context)
  {
    _context = context;
    _dbSet = context.Set<T>();
  }

  public async Task<T> Update(T entidade)
  {
    _dbSet.Update(entidade);
    await _context.SaveChangesAsync();
    return entidade;
  }

  public async Task<T> Create(T entidade)
  {
    await _dbSet.AddAsync(entidade);
    await _context.SaveChangesAsync();

    return entidade;
  }

  public async Task<T> Delete(Guid guid)
  {
    var entity = await GetById(guid);
    if (entity is null)
    {
      return entity;
    }
    _dbSet.Remove(entity);
    await _context.SaveChangesAsync();

    return entity;
  }

  public async Task<T> GetById(Guid guid)
  {
    var get = await _dbSet.FirstOrDefaultAsync(entity => entity.Guid == guid);

    return get;
  }

  public async Task<IList<T>> GetAll()
  {
    return await _dbSet.ToListAsync();
  }
}
