using Infrastructure.Repository.EFRepository;
using Microsoft.EntityFrameworkCore;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Repository;

namespace TechChallenge.Infrastructure.Repository;
public class ContactRepository : EFRepository<Contact>, IContactRepository
{
  public ContactRepository(ApplicationDbContext.ApplicationDbContext context) : base(context)
  {

  }

  public async Task UpdateContact(Contact contact)
  {
    var oldContact = await GetContactById(contact.Guid);

    if(oldContact is null)
    {
      return;
    }

    oldContact.Name = contact.Name;
    oldContact.Phone = contact.Phone;
    oldContact.Region = contact.Region;
    oldContact.Email = contact.Email;

    await _context.SaveChangesAsync();    
  }

  public async Task<List<Contact>> GetContactByRegion(int ddd)
  {

    var conctacts = await _context.Contact
      .Where(x => x.RegionDDD == ddd)
      .Include(x => x.Region)
      .AsNoTracking().Select(
      x => new Contact 
        { 
        Email = x.Email,
        Guid = x.Guid,
        Name = x.Name,
        Phone = x.Phone,
        Region = x.Region,
        RegionDDD = x.RegionDDD 
      })
      .ToListAsync();

    if (conctacts is null)
    {
      return new List<Contact>();
    }

    return conctacts;
  }

  public async Task<List<Contact>> GetAllContacts()
  {

    var conctacts = await _context.Contact     
      .Include(x => x.Region)
      .AsNoTracking().Select(
      x => new Contact
      {
        Email = x.Email,
        Guid = x.Guid,
        Name = x.Name,
        Phone = x.Phone,
        Region = x.Region,
        RegionDDD = x.RegionDDD
      })
      .ToListAsync();

    if (conctacts is null)
    {
      return new List<Contact>();
    }

    return conctacts;
  }

  public async Task CreateContact(Contact contact)
  {
    await _dbSet.AddAsync(contact);
    await _context.SaveChangesAsync();
  }

  public async Task DeleteContact(Guid guid)
  {
    var entity = await GetContactById(guid);
    if (entity is null)
    {
      return;
    }
    _dbSet.Remove(entity);
    await _context.SaveChangesAsync();
  }

  public async Task<Contact?> GetContactById(Guid guid)
  {
    var get = await _dbSet.FirstOrDefaultAsync(entity => entity.Guid == guid);

    return get;
  }

}
