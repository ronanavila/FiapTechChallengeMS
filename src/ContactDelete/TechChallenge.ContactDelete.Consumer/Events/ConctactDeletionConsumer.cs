using MassTransit;
using TechChallenge.ContactDelete.Consumer.Services;
using TechChallenge.Domain.Entities;

namespace TechChallenge.ContactDelete.Consumer.Events;
public class ConctactDeletionConsumer : IConsumer<ContactGuidClass>
{
  private readonly IDeleteContactService _deleteContactService;
  private readonly ILogger<Worker> _logger;
  public ConctactDeletionConsumer(IDeleteContactService deleteContactService, ILogger<Worker> logger)
  {
    _deleteContactService = deleteContactService;
    _logger = logger;
  }

  public async Task Consume(ConsumeContext<ContactGuidClass> context)
  {
    try
    {
      if (context is not null)
      {
        await _deleteContactService.DeleteContact(context.Message.ContactGuid);
        _logger.LogInformation("Contato deletado com sucesso");
      }
    }
    catch (Exception ex)
    {
      _logger.LogError("Erro ao deletar contato");
    }
  }
}