using MassTransit;
using Microsoft.AspNetCore.Mvc;
using TechChallenge.ContactUpdate.Application.DTO;
using TechChallenge.ContactUpdate.Application.Services;
using TechChallenge.ContactUpdate.Controller;
using TechChallenge.Domain.Shared;

namespace TechChallenge.Api.Controllers;
[Route("api/contacts/update")]
[ApiController]
public class ContactUpdateController : ControllerBase
{
  private readonly IContactService _contactService;
  private readonly IBus _bus;

  public ContactUpdateController(IBus bus, IContactService contactService)
  {
    _contactService = contactService;
    _bus = bus;
  }


  /// <summary>
  /// Update a contact
  /// </summary>
  /// <response code="200">Returns uid from a newly contact created</response>
  /// <response code="400">If the parameters are wrong</response>
  /// <response code="404">Contact not found</response>
  /// <response code="500">Unexpected Error</response>
  [HttpPut()]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> UpdateContact([FromBody] ContactUpdateDTO contactDto)
  {

    var result = await _contactService.UpdateContact(contactDto);

    if (result.Success)
    {
      var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{Configuration.QueueName}"));

      await endpoint.Send(result.Data);

      return StatusCode((int)result.StatusCode, result);
    }

    return StatusCode((int)result.StatusCode, result);
  }
}
