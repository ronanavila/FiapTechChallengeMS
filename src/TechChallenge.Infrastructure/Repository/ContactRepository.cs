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

  public async Task<Contact> UpdateContact(Contact contact)
  {
    var oldContact = await GetById(contact.Guid);

    if(oldContact is null)
    {
      return new Contact();
    }

    oldContact.Name = contact.Name;
    oldContact.Phone = contact.Phone;
    oldContact.Region = contact.Region;
    oldContact.Email = contact.Email;

    await _context.SaveChangesAsync();
    return contact;
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

  public async Task<Guid> CreateContact(Contact contact)
  {
    await _dbSet.AddAsync(contact);
    await _context.SaveChangesAsync();

    return contact.Guid;
  }

}
