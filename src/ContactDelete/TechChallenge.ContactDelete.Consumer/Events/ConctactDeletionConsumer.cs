using MassTransit;
using TechChallenge.Domain.Entities;

namespace TechChallenge.ContactDelete.Consumer.Events;
public class ConctactDeletionConsumer : IConsumer<ContactGuidClass>
{
  public Task Consume(ConsumeContext<ContactGuidClass> context)
  {
    Console.WriteLine(context);
    return Task.CompletedTask;
  }
}