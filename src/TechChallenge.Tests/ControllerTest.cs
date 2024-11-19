namespace TechChallenge.Tests;

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
        var controller = new ContactController(_mockContactService.Object);

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
        var controller = new ContactController(_mockContactService.Object);

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
        var controller = new ContactController(_mockContactService.Object);

        //Act
        var actual = await controller.GetContactByRegion(It.IsAny<int>());

        //Assert
        var statusCodeResult = actual as ObjectResult;
        Assert.Equal((int)response.StatusCode, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task RemoveContact_Success_Test()
    {
        //Arange 
        response.StatusCode = HttpStatusCode.OK;
        _mockContactService.Setup(x => x.Delete(It.IsAny<Guid>())).ReturnsAsync(response);
        var controller = new ContactController(_mockContactService.Object);

        //Act
        var actual = await controller.RemoveContact(It.IsAny<Guid>());

        //Assert
        var statusCodeResult = actual as ObjectResult;
        Assert.Equal((int)response.StatusCode, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task UpdateContact_Success_Test()
    {
        //Arange 
        response.StatusCode = HttpStatusCode.OK;
        _mockContactService.Setup(x => x.UpdateContact(It.IsAny<ContactUpdateDTO>())).ReturnsAsync(response);
        var controller = new ContactController(_mockContactService.Object);

        //Act
        var actual = await controller.UpdateContact(It.IsAny<ContactUpdateDTO>());

        //Assert
        var statusCodeResult = actual as ObjectResult;
        Assert.Equal((int)response.StatusCode, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task CreateContact_Success_Test()
    {
        //Arange 
        response.StatusCode = HttpStatusCode.OK;
        _mockContactService.Setup(x => x.CreateContact(It.IsAny<ContactCreationDTO>())).ReturnsAsync(response);
        var controller = new ContactController(_mockContactService.Object);

        //Act
        var actual = await controller.CreateContact(It.IsAny<ContactCreationDTO>());

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
        var controller = new ContactController(_mockContactService.Object);

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
        var controller = new ContactController(_mockContactService.Object);

        //Act
        var actual = await controller.GetContactByRegion(It.IsAny<int>());

        //Assert
        var statusCodeResult = actual as ObjectResult;
        Assert.Equal((int)response.StatusCode, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task RemoveContact_Error_Test()
    {
        //Arange 
        response.StatusCode = HttpStatusCode.InternalServerError;
        _mockContactService.Setup(x => x.Delete(It.IsAny<Guid>())).ReturnsAsync(response);
        var controller = new ContactController(_mockContactService.Object);

        //Act
        var actual = await controller.RemoveContact(It.IsAny<Guid>());

        //Assert
        var statusCodeResult = actual as ObjectResult;
        Assert.Equal((int)response.StatusCode, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task UpdateContact_Error_Test()
    {
        //Arange 
        response.StatusCode = HttpStatusCode.InternalServerError;
        _mockContactService.Setup(x => x.UpdateContact(It.IsAny<ContactUpdateDTO>())).ReturnsAsync(response);
        var controller = new ContactController(_mockContactService.Object);

        //Act
        var actual = await controller.UpdateContact(It.IsAny<ContactUpdateDTO>());

        //Assert
        var statusCodeResult = actual as ObjectResult;
        Assert.Equal((int)response.StatusCode, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task CreateContact_Error_Test()
    {
        //Arange 
        response.StatusCode = HttpStatusCode.InternalServerError;
        _mockContactService.Setup(x => x.CreateContact(It.IsAny<ContactCreationDTO>())).ReturnsAsync(response);
        var controller = new ContactController(_mockContactService.Object);

        //Act
        var actual = await controller.CreateContact(It.IsAny<ContactCreationDTO>());

        //Assert
        var statusCodeResult = actual as ObjectResult;
        Assert.Equal((int)response.StatusCode, statusCodeResult.StatusCode);
    }

    #endregion
}