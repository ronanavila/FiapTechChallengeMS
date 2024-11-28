using MassTransit;
using TechChallenge.Domain.Entities;

namespace TechChallenge.ContactUpdate.Consumer.Events;
public class ContactUpdateConsumer : IConsumer<Contact>
{
  public Task Consume(ConsumeContext<Contact> context)
  {
    Console.WriteLine(context);
    return Task.CompletedTask;
  }
}
