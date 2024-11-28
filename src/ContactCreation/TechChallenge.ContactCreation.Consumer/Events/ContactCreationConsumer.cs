using MassTransit;
using TechChallenge.Domain.Entities;

namespace TechChallenge.ContactCreation.Consumer.Events;
public class ContactCreationConsumer : IConsumer<Contact>
{
  public Task Consume(ConsumeContext<Contact> context)
  {
    Console.WriteLine(context);
    return Task.CompletedTask;
  }
}
