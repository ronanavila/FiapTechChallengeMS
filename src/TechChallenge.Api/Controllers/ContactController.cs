using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechChallenge.Application.DTO;
using TechChallenge.Application.Services;
using TechChallenge.Domain.Shared;

namespace TechChallenge.Api.Controllers;
[Route("api/contacts")]
[ApiController]
public class ContactController : ControllerBase
{
  private readonly IContactService _contactService;

  public ContactController(IContactService contactService)
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
  [ProducesResponseType(typeof(BaseResponse),StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> CreateContact([FromBody] ContactCreationDTO request)
  {

    var result = await _contactService.CreateContact(request);

    return StatusCode((int)result.StatusCode, result);
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

    return StatusCode((int)result.StatusCode, result);
  }
}
