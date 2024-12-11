using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using TechChallenge.ContactUpdate.Application.DTO;
using TechChallenge.Domain.Entities;
using TechChallenge.Infrastructure.Repository.ApplicationDbContext;


namespace TechChallenge.ContactUpdate.IntegrationTests;

public class ContactsControllerTests
{
  [Fact]
  public async Task ContactsUpdateContact()
  {
    //Arrange
    var application = new ContactUpdateWebApplictionFactory();
    var client = application.CreateClient();
    using var scope = application.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    var contactToChange = await db.Contact.FirstOrDefaultAsync<Contact>();
    ContactUpdateDTO contact = new ContactUpdateDTO(contactToChange.Guid, "Igor", "igor@igor.com", 12, "888889999");

    //Act
    var response = await client.PutAsJsonAsync("/api/contacts/update", contact);

    //Assert
    response.EnsureSuccessStatusCode();

    var matchResponse = await response.Content.ReadFromJsonAsync<TestResponses<ContactResponseDTO>>();

    Assert.True(matchResponse?.Success);
    Assert.Null(matchResponse?.Errors);


    Thread.Sleep(5000);

    using var scopeUpdated = application.Services.CreateScope();
    var dbUpdated = scopeUpdated.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    var contactUpdated = await dbUpdated.Contact.FirstOrDefaultAsync<Contact>(c => c.Guid == contactToChange.Guid);

    Assert.NotNull(contactUpdated);
    Assert.Equal("Igor", contactUpdated.Name);
    Assert.Equal("igor@igor.com", contactUpdated.Email);
    Assert.Equal("888889999", contactUpdated.Phone);
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