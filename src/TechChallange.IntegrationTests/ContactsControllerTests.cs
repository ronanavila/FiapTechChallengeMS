using Flunt.Notifications;
using Microsoft.Identity.Client;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using TechChallenge.Application.DTO;
using TechChallenge.Domain.Contracts;
using TechChallenge.Domain.Shared;

namespace TechChallange.IntegrationTests;

public class ContactsControllerTests
{
  [Fact]
  public async Task ContactsCreateNewContact()
  {
    //Arrange
    var application = new ContactsWebApplictionFactory();
    ContactCreationDTO contact = new("João", "joao@gmail.com", 12, "999998888");
    var client = application.CreateClient();
    //Act
    var response = await client.PostAsJsonAsync("/api/contacts", contact);
    //Assert
    response.EnsureSuccessStatusCode();

    var matchResponse = await response.Content.ReadFromJsonAsync<TestResponses<Guid>>();

    Assert.True(matchResponse?.Success);
    Assert.Null(matchResponse?.Errors);
    Assert.IsType<Guid>(matchResponse?.Data);
  }

  [Fact]
  public async Task ContactsGetAllContacts()
  {
    //Arrange
    var application = new ContactsWebApplictionFactory();
    var client = application.CreateClient();
    //Act
    var response = await client.GetAsync("/api/contacts");
    //Assert
    response.EnsureSuccessStatusCode();

    var matchResponse = await response.Content.ReadFromJsonAsync<TestResponses<List<ContactResponseDTO>>>();

    Assert.True(matchResponse?.Success);
    Assert.Null(matchResponse?.Errors);
    Assert.True(matchResponse?.Data.Count > 0);
  }

  [Fact]
  public async Task ContactsGetContactByRegion()
  {
    //Arrange
    var application = new ContactsWebApplictionFactory();
    var client = application.CreateClient();
    //Act
    var response = await client.GetAsync("/api/contacts/12");
    //Assert
    response.EnsureSuccessStatusCode();

    var matchResponse = await response.Content.ReadFromJsonAsync<TestResponses<List<ContactResponseDTO>>>();

    Assert.True(matchResponse?.Success);
    Assert.Null(matchResponse?.Errors);
    Assert.True(matchResponse?.Data.Count > 0);
  }

  [Fact]
  public async Task ContactsRemoveContact()
  {
    //Arrange
    var application = new ContactsWebApplictionFactory();
    var client = application.CreateClient();
 
    var getResponse = await client.GetAsync("/api/contacts");
    var getMatchResponse = await getResponse.Content.ReadFromJsonAsync<TestResponses<List<ContactResponseDTO>>>();
    var guid = getMatchResponse.Data.FirstOrDefault().Guid;

    //Act
    var response = await client.DeleteAsync($"/api/contacts/{guid}");

    //Assert
    response.EnsureSuccessStatusCode();

    var matchResponse = await response.Content.ReadFromJsonAsync<TestResponses<ContactResponseDTO>>();

    Assert.True(matchResponse?.Success);
    Assert.Null(matchResponse?.Errors);
    Assert.Equal(guid, matchResponse.Data.Guid);
  }

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