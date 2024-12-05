using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using TechChallenge.ContactUpdate.Application.DTO;
using TechChallenge.ContactUpdate.Application.Services;
using TechChallenge.ContactUpdate.Controller.Controllers;
using TechChallenge.Domain.Shared;

namespace TechChallenge.ContactUpdate.Tests;

public class ControllerTest
{
  private readonly Mock<IBus> _bus;
  private readonly BaseResponse response;
  private readonly Mock<IContactService> _mockContactService;
  private ContactUpdateDTO _contactCreationDTO;

  public ControllerTest()
  {
    _bus = new Mock<IBus>();
    _mockContactService = new Mock<IContactService>();
    response = new BaseResponse();

    var mockSendEndpoint = new Mock<ISendEndpoint>();

    mockSendEndpoint
        .Setup(endpoint => endpoint.Send(It.IsAny<ContactUpdateDTO>(), default))
        .Returns(Task.CompletedTask)
        .Verifiable();


    _bus.Setup(bus => bus.GetSendEndpoint(It.IsAny<Uri>()))
          .ReturnsAsync(mockSendEndpoint.Object);
    _contactCreationDTO = new ContactUpdateDTO(Guid.NewGuid(), "joao", "joao@peterson.com.br", 12, "666665555");
  }

  #region Success

  [Fact]
  public async Task UpdateContact_Success_Test()
  {
    //Arange 
    response.StatusCode = HttpStatusCode.Accepted;
    response.Success = true;
    _mockContactService.Setup(x => x.UpdateContact(_contactCreationDTO)).ReturnsAsync(response);
    var controller = new ContactUpdateController(_bus.Object, _mockContactService.Object);

    //Act
    var actual = await controller.UpdateContact(_contactCreationDTO);

    //Assert
    var statusCodeResult = actual as ObjectResult;
    Assert.Equal((int)response.StatusCode, statusCodeResult.StatusCode);
  }

  #endregion

  #region Error   

  [Fact]
  public async Task UpdateContact_Error_Test()
  {
    //Arange 
    response.Success = false;
    _mockContactService.Setup(x => x.UpdateContact(_contactCreationDTO)).ReturnsAsync(response);
    var controller = new ContactUpdateController(_bus.Object, _mockContactService.Object);

    //Act
    var actual = await controller.UpdateContact(_contactCreationDTO);

    //Assert
    var statusCodeResult = actual as ObjectResult;
    Assert.Equal((int)response.StatusCode, statusCodeResult.StatusCode);
  }
  #endregion
}