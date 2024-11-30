using MassTransit;
using TechChallenge.ContactUpdate.Consumer.Services;
using TechChallenge.Domain.Entities;

namespace TechChallenge.ContactUpdate.Consumer.Events;
public class ContactUpdateConsumer : IConsumer<Contact>
{
  private readonly IUpdateContactService _updateContactService;
  private readonly ILogger<Worker> _logger;

  public ContactUpdateConsumer(IUpdateContactService updateContactService, ILogger<Worker> logger)
  {
    _updateContactService = updateContactService;
    _logger = logger;
  }

  public async Task Consume(ConsumeContext<Contact> context)
  {
    try
    {
      if (context is not null)
      {
        await _updateContactService.UpdateContact(context.Message);
        _logger.LogInformation("Contato criado com sucesso");
      }
    }
    catch (Exception ex)
    {
      _logger.LogError("Erro ao criar contato");
    }
  }
}
