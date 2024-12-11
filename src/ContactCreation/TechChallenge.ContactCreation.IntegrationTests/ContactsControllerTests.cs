using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using TechChallenge.ContactCreation.Application.DTO;
using TechChallenge.Domain.Entities;
using TechChallenge.Infrastructure.Repository.ApplicationDbContext;


namespace TechChallenge.ContactCreation.IntegrationTests;

public class ContactsControllerTests
{
  [Fact]
  public async Task ContactsCreateNewContact()
  {
    //Arrange
    var application = new ContactCreationWebApplictionFactory();
    ContactCreationDTO contact = new("João", "paulosergio@paulosergio.com", 12, "999998888");
    var client = application.CreateClient();
    //Act
    var response = await client.PostAsJsonAsync("/api/contacts/creation", contact);
    //Assert
    response.EnsureSuccessStatusCode();

    var matchResponse = await response.Content.ReadFromJsonAsync<TestResponses<Contact>>();

    Assert.True(matchResponse?.Success);
    Assert.Null(matchResponse?.Errors);
    Assert.IsType<Contact>(matchResponse?.Data);


    Thread.Sleep(5000);

    using var scope = application.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var clienteCreated = await db.Contact.FirstOrDefaultAsync<Contact>(c => c.Email == "paulosergio@paulosergio.com");

    Assert.NotNull(clienteCreated);
    Assert.Equal("paulosergio@paulosergio.com", clienteCreated.Email);

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