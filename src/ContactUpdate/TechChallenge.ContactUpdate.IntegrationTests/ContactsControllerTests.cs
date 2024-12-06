using Flunt.Notifications;
using System.Net;
using System.Net.Http.Json;
using TechChallenge.ContactUpdate.Application.DTO;


namespace TechChallenge.ContactUpdate.IntegrationTests;

public class ContactsControllerTests
{
  [Fact]
  public async Task ContactsUpdateContact()
  {
    //Arrange
    var application = new ContactUpdateWebApplictionFactory();
    var client = application.CreateClient();

  
    ContactUpdateDTO contact = new ContactUpdateDTO(Guid.Parse("abf91f63-af68-4856-bd87-09014f894c69"), "Igor", "igor@igor.com", 12, "888889999");

    //Act
    var response = await client.PutAsJsonAsync("/api/contacts/update", contact);

    //Assert
    response.EnsureSuccessStatusCode();

    var matchResponse = await response.Content.ReadFromJsonAsync<TestResponses<ContactResponseDTO>>();

    Assert.True(matchResponse?.Success);
    Assert.Null(matchResponse?.Errors);
  }
}



internal class TestResponses<T>
{
  public bool Success { get; set; }
  public string Message { get; set; }
  public T Data { get; set; }
  public IReadOnlyCollection<Notification>? Errors { get; set; }
  public HttpStatusCode StatusCode { get; set; }
}