using MassTransit;
using TechChallenge.ContactCreation.Consumer.Services;
using TechChallenge.Domain.Entities;

namespace TechChallenge.ContactCreation.Consumer.Events;
public class ContactCreationConsumer : IConsumer<Contact>
{
  private readonly ICreateContactService _createContactService;
  private readonly ILogger<Worker> _logger;
  public ContactCreationConsumer(ICreateContactService createContactService, ILogger<Worker> logger)
  {
    _createContactService = createContactService;
    _logger = logger;
  }

  public async Task Consume(ConsumeContext<Contact> context)
  {
    try
    {
      if (context is not null)
      {
       await _createContactService.CreateContact(context.Message);
        _logger.LogInformation("Contato criado com sucesso");
      }
    }
    catch (Exception ex)
    {
      _logger.LogError("Erro ao criar contato");
    }
  }
}
