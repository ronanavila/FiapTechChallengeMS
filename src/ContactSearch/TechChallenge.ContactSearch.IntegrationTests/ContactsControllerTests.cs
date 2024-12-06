using Flunt.Notifications;
using System.Net;
using System.Net.Http.Json;
using TechChallenge.ContactSearch.Application.DTO;


namespace TechChallenge.ContactSearch.IntegrationTests;

public class ContactsControllerTests
{
  [Fact]
  public async Task ContactsGetAllContacts()
  {
    //Arrange
    var application = new ContactSearchWebApplictionFactory();
    var client = application.CreateClient();
    //Act
    var response = await client.GetAsync("/api/contacts/search");
  
    response.EnsureSuccessStatusCode();

    var matchResponse = await response.Content.ReadFromJsonAsync<TestResponses<List<ContactResponseDTO>>>();
      //Assert
    Assert.True(matchResponse?.Success);
    Assert.Null(matchResponse?.Errors);
    Assert.True(matchResponse?.Data.Count > 0);
  }

  [Fact]
  public async Task ContactsGetContactByRegion()
  {
    //Arrange
    var application = new ContactSearchWebApplictionFactory();
    var client = application.CreateClient();
    //Act
    var response = await client.GetAsync("/api/contacts/search/12");
    //Assert
    response.EnsureSuccessStatusCode();

    var matchResponse = await response.Content.ReadFromJsonAsync<TestResponses<List<ContactResponseDTO>>>();

    Assert.True(matchResponse?.Success);
    Assert.Null(matchResponse?.Errors);
    Assert.True(matchResponse?.Data.Count > 0);
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