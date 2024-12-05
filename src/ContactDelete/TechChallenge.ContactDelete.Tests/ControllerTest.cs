using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using TechChallenge.ContactDelete.Controller.Controllers;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Shared;

namespace TechChallenge.ContactDelete.Tests;

public class ControllerTest
{
  private readonly BaseResponse response;
  private readonly Mock<IBus> _bus;

  public ControllerTest()
  {
    response = new BaseResponse();
    _bus = new Mock<IBus>();
    var mockSendEndpoint = new Mock<ISendEndpoint>();

    mockSendEndpoint
        .Setup(endpoint => endpoint.Send(It.IsAny<ContactGuidClass>(), default))
        .Returns(Task.CompletedTask)
        .Verifiable();


    _bus.Setup(bus => bus.GetSendEndpoint(It.IsAny<Uri>()))
          .ReturnsAsync(mockSendEndpoint.Object);
  }

  #region Success
  [Fact]
  public async Task RemoveContact_Success_Test()
  {
    //Arange 
    response.StatusCode = HttpStatusCode.Accepted;
    var controller = new ContactDeleteController(_bus.Object);
    var guid = Guid.NewGuid();

    //Act
    var actual = await controller.RemoveContact(guid);

    //Assert
    var statusCodeResult = actual as ObjectResult;
    Assert.Equal((int)response.StatusCode, statusCodeResult.StatusCode);
  }

  #endregion
}