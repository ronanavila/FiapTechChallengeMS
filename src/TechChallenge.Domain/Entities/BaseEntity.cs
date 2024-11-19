namespace TechChallenge.Domain.Entities;
public class BaseEntity
{
  public Guid Guid { get; set; } = Guid.NewGuid();
}
