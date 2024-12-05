using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using TechChallenge.ContactSearch.Application.Services;
using TechChallenge.ContactSearch.Controller.Controllers;
using TechChallenge.Domain.Shared;

namespace TechChallenge.ContactSearch.Tests;

public class ControllerTest
{
    private readonly Mock<IContactService> _mockContactService;
    private readonly BaseResponse response;

    public ControllerTest()
    {
        _mockContactService = new Mock<IContactService>();
        response = new BaseResponse();
    }

    #region Success
    [Fact]
  public async Task GetAll_Success_Test()
  {
        //Arange 
        response.StatusCode = HttpStatusCode.OK;
        _mockContactService.Setup(x => x.GetAllContacts()).ReturnsAsync(response);
        var controller = new ContactSearchController(_mockContactService.Object);

        //Act
        var actual = await controller.GetAll();

        //Assert
        var statusCodeResult = actual as ObjectResult;
        Assert.Equal((int)response.StatusCode, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task GetContactByRegion_Success_Test()
    {
        //Arange 
        response.StatusCode = HttpStatusCode.OK;
        _mockContactService.Setup(x => x.GetContactByRegion(It.IsAny<int>())).ReturnsAsync(response);
        var controller = new ContactSearchController(_mockContactService.Object);

        //Act
        var actual = await controller.GetContactByRegion(It.IsAny<int>());

        //Assert
        var statusCodeResult = actual as ObjectResult;
        Assert.Equal((int)response.StatusCode, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task GetContactByRegion_NotFound_Test()
    {
        //Arange 
        response.StatusCode = HttpStatusCode.NotFound;
        _mockContactService.Setup(x => x.GetContactByRegion(It.IsAny<int>())).ReturnsAsync(response);
        var controller = new ContactSearchController(_mockContactService.Object);

        //Act
        var actual = await controller.GetContactByRegion(It.IsAny<int>());

        //Assert
        var statusCodeResult = actual as ObjectResult;
        Assert.Equal((int)response.StatusCode, statusCodeResult.StatusCode);
    }



   
    #endregion

    #region Error
    [Fact]
    public async Task GetAll_Error_Test()
    {
        //Arange 
        response.StatusCode = HttpStatusCode.InternalServerError;
        _mockContactService.Setup(x => x.GetAllContacts()).ReturnsAsync(response);
        var controller = new ContactSearchController(_mockContactService.Object);

        //Act
        var actual = await controller.GetAll();

        //Assert
        var statusCodeResult = actual as ObjectResult;
        Assert.Equal((int)response.StatusCode, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task GetContactByRegion_Error_Test()
    {
        //Arange 
        response.StatusCode = HttpStatusCode.InternalServerError;
        _mockContactService.Setup(x => x.GetContactByRegion(It.IsAny<int>())).ReturnsAsync(response);
        var controller = new ContactSearchController(_mockContactService.Object);

        //Act
        var actual = await controller.GetContactByRegion(It.IsAny<int>());

        //Assert
        var statusCodeResult = actual as ObjectResult;
        Assert.Equal((int)response.StatusCode, statusCodeResult.StatusCode);
    }  

    #endregion
}