using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using System.Threading;
using TechChallenge.ContactCreation.Application.DTO;
using TechChallenge.ContactCreation.Application.Services;
using TechChallenge.ContactCreation.Controller.Controllers;
using TechChallenge.Domain.Shared;

namespace TechChallenge.ContactCreation.Tests;

public class ControllerTest
{
  private readonly Mock<IContactService> _mockContactService;
  private readonly  BaseResponse response;
  private readonly Mock<IBus> _bus;
  private ContactCreationDTO _contactCreationDTO;

  public ControllerTest()
  {
    _bus = new Mock<IBus>();
    _mockContactService = new Mock<IContactService>();
    response = new BaseResponse();

    var mockSendEndpoint = new Mock<ISendEndpoint>();

    mockSendEndpoint
        .Setup(endpoint => endpoint.Send(It.IsAny<ContactCreationDTO>(), default))
        .Returns(Task.CompletedTask)
        .Verifiable();


    _bus.Setup(bus => bus.GetSendEndpoint(It.IsAny<Uri>()))
          .ReturnsAsync(mockSendEndpoint.Object);
    _contactCreationDTO = new ContactCreationDTO("joao", "joao@peterson.com.br", 12, "666665555");

  }


  #region Success

  [Fact]
  public async Task CreateContact_Success_Test()
  {

      //Arange 
    response.Success = true;
    _mockContactService.Setup(x => x.CreateContactValidation(_contactCreationDTO)).ReturnsAsync(response);
  
    var controller = new ContactCreationController(_bus.Object, _mockContactService.Object);

    //Act
    var actual = await controller.CreateContact(_contactCreationDTO);

    //Assert
    var statusCodeResult = actual as ObjectResult;
    Assert.Equal((int)response.StatusCode, statusCodeResult.StatusCode);
  }
  #endregion

  #region Error


  [Fact]
  public async Task CreateContact_Error_Test()
  {
    //Arange 
    response.Success = false;
    _mockContactService.Setup(x => x.CreateContactValidation(_contactCreationDTO)).ReturnsAsync(response);
    var controller = new ContactCreationController(_bus.Object,_mockContactService.Object);

    //Act
    var actual = await controller.CreateContact(_contactCreationDTO);

    //Assert
    var statusCodeResult = actual as ObjectResult;
    Assert.Equal((int)response.StatusCode, statusCodeResult.StatusCode);
  }

  #endregion
}