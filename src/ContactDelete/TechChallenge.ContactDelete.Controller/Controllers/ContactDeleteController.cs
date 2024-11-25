using MassTransit;
using Microsoft.AspNetCore.Mvc;
using TechChallenge.ContactDelete.Controller;
using TechChallenge.ContactDelete.Application.Services;
using TechChallenge.Domain.Shared;
using static MassTransit.Monitoring.Performance.BuiltInCounters;
using System.Text;

namespace TechChallenge.Api.Controllers;
[Route("api/contacts/delete")]
[ApiController]
public class ContactDeleteController : ControllerBase
{
  private readonly IBus _bus;
  public ContactDeleteController(IBus bus)
  {

    _bus = bus;
  }

  /// <summary>
  /// Delete a contact by guid
  /// </summary>
  /// <param name="guid"></param>
  /// <response code="200">Returns uid from a newly contact created</response>
  /// <response code="404">Contact not found</response>
  /// <response code="500">Unexpected Error</response>
  [HttpDelete("{guid:Guid}")]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> RemoveContact([FromRoute] Guid guid)
  { 
      var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{Configuration.QueueName}"));

    await endpoint.Send(new Teste(guid));

      return StatusCode(202, "");
  }
}


public class Teste
{
    public Teste(Guid guid)
    {
    ContactGuid = guid;
    }

    public Guid ContactGuid { get; set; }
}
