using Flunt.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Json;


namespace TechChallenge.ContactDelete.IntegrationTests;

public class ContactsControllerTests
{
  [Fact]
  public async Task ContactsRemoveContact()
  {
    //Arrange
    var application = new ContactDeleteWebApplictionFactory();
    var client = application.CreateClient();

    //Act
    var response = await client.DeleteAsync($"/api/contacts/delete/abf91f63-af68-4856-bd87-09014f894c69");

    //Assert
    response.EnsureSuccessStatusCode();

    Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
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