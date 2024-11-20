using Microsoft.AspNetCore.Mvc;
using TechChallenge.ContactDelete.Application.Services;
using TechChallenge.Domain.Shared;

namespace TechChallenge.Api.Controllers;
[Route("api/contacts/delete")]
[ApiController]
public class ContactDeleteController : ControllerBase
{
  private readonly IContactService _contactService;

  public ContactDeleteController(IContactService contactService)
  {
    _contactService = contactService;
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

    var result = await _contactService.Delete(guid);

    return StatusCode((int)result.StatusCode, result);
  }
}
