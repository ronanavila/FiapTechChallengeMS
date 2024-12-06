using Flunt.Notifications;
using System.Net;
using System.Net.Http.Json;
using TechChallenge.ContactCreation.Application.DTO;
using TechChallenge.ContactUpdate.Application.DTO;


namespace TechChallenge.ContactUpdate.IntegrationTests;

public class ContactsControllerTests
{
  [Fact]
  public async Task ContactsUpdateContact()
  {
    //Arrange
    var application = new ContactsWebApplictionFactory();
    var client = application.CreateClient();

    var getResponse = await client.GetAsync("/api/contacts");
    getResponse.EnsureSuccessStatusCode();
    var getMatchResponse = await getResponse.Content.ReadFromJsonAsync<TestResponses<List<ContactResponseDTO>>>();
    var guid = getMatchResponse.Data.FirstOrDefault().Guid;

    ContactUpdateDTO contact = new ContactUpdateDTO(guid, "João", "joao@gmail.com", 12, "999998888");

    //Act
    var response = await client.PutAsJsonAsync("/api/contacts", contact);

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