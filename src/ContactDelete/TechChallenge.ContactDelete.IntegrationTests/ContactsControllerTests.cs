using Flunt.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using TechChallenge.Domain.Entities;
using TechChallenge.Infrastructure.Repository.ApplicationDbContext;


namespace TechChallenge.ContactDelete.IntegrationTests;

public class ContactsControllerTests
{
  [Fact]
  public async Task ContactsRemoveContact()
  {
    //Arrange
    var application = new ContactDeleteWebApplictionFactory();
    var client = application.CreateClient();
    using var scope = application.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    var contactToDelete = await db.Contact.FirstOrDefaultAsync<Contact>();
    
    //Act
    var response = await client.DeleteAsync($"/api/contacts/delete/{contactToDelete.Guid}");

    //Assert
    response.EnsureSuccessStatusCode();

    Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);

    Thread.Sleep(5000);

    using var scopeDeleted = application.Services.CreateScope();
    var dbDeleted = scopeDeleted.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var contactDeleted = await db.Contact.FirstOrDefaultAsync<Contact>(c => c.Guid == contactToDelete.Guid);

    Assert.Null(contactDeleted);
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