using Microsoft.AspNetCore.Mvc;
using TechChallenge.ContactCreation.Application.DTO;
using TechChallenge.ContactCreation.Application.Services;
using TechChallenge.Domain.Shared;

namespace TechChallenge.ContactCreation.Controller.Controllers;
[Route("api/contacts/creation")]
[ApiController]
public class ContactCreationController : ControllerBase
{

  private readonly IContactService _contactService;

  public ContactCreationController(IContactService contactService)
  {
    _contactService = contactService;
  }
  /// <summary>
  /// Create a Contact.
  /// </summary>
  /// <returns>Return a contact Guid</returns>
  /// <response code="201">Returns uid from a newly contact created</response>
  /// <response code="400">If the parameters are wrong</response>
  /// <response code="500">Unexpected Error</response>
  [HttpPost]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> CreateContact([FromBody] ContactCreationDTO request)
  {

    var result = await _contactService.CreateContact(request);

    return StatusCode((int)result.StatusCode, result);
  }
}
