namespace TechChallenge.Domain.Entities;
public class Region
{
  public int DDD { get; set; }
  public string Location { get; set; } = string.Empty;

  public virtual IList<Contact> Contacts { get; } = new List<Contact>();

  public Region()
  {

  }

  public Region(int ddd, string location)
  {
    DDD = ddd;
    Location = location;
  }
}
