using Microsoft.AspNetCore.Mvc;
using TechChallenge.ContactSearch.Application.Services;
using TechChallenge.Domain.Shared;

namespace TechChallenge.Api.Controllers;
[Route("api/contacts/search")]
[ApiController]
public class ContactSearchController : ControllerBase
{
  private readonly IContactService _contactService;

  public ContactSearchController(IContactService contactService)
  {
    _contactService = contactService;
  }

  /// <summary>
  /// Find all contacts
  /// </summary>
  /// <returns>Return a contact Guid</returns>
  /// <response code="200">Returns uid from a newly contact created</response>
  /// <response code="404">Contact not found</response>
  /// <response code="500">Unexpected Error</response>
  [HttpGet]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetAll()
  {
    var result = await _contactService.GetAllContacts();

    return StatusCode((int)result.StatusCode, result);
  }

  /// <summary>
  /// Find contacts by DDD
  /// </summary>
  /// <param name="ddd"></param>
  /// <response code="200">Returns uid from a newly contact created</response>
  /// <response code="404">Contact not found</response>
  /// <response code="500">Unexpected Error</response>
  [HttpGet("{ddd:int}")]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetContactByRegion([FromRoute] int ddd)
  {

    var result = await _contactService.GetContactByRegion(ddd);

    return StatusCode((int)result.StatusCode, result);

  }
}
