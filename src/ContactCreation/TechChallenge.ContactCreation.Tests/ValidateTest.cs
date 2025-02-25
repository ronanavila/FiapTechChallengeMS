using TechChallenge.ContactCreation.Application.DTO;
using TechChallenge.ContactCreation.Application.Services;

namespace TechChallenge.ContactCreation.Tests;

public class ValidateTest
{
  private readonly ContactService contactService;
  public ValidateTest()
  {
    contactService = new ContactService();
  }

  [Theory]
  [InlineData("E9999999", "Informe somente n�meros no telefone")]
  [InlineData("9999999", "Telefone tem que ter no minimo 8 n�meros.")]
  [InlineData("9999999999", "Telefone tem que ter no m�ximo 9 n�meros.")]
  public async Task CreateContact_Invalid_Phone(string phone, string error)
  {
    //Arange 
    var request = new ContactCreationDTO("Marie", "test@gmail.com", 31, phone);

    //Act
    var actual = await contactService.CreateContactValidation(request);

    //Assert
    Assert.Equal(error, actual.Errors.First().Message);
    Assert.Equal("BadRequest", actual.StatusCode.ToString());
  }

  [Theory]
  [InlineData(09, "DDD est� abaixo da faixa no Brasil.")]
  [InlineData(101, "DDD est� acima da faixa no Brasil.")]
  public async Task CreateContact_Invalid_DDD(int ddd, string error)
  {
    //Arange 
    var request = new ContactCreationDTO("Marie", "test@gmail.com", ddd, "99999999");

    //Act
    var actual = await contactService.CreateContactValidation(request);

    //Assert
    Assert.Contains(error, actual.Errors.First().Message);
    Assert.Equal("BadRequest", actual.StatusCode.ToString());
  }

  [Fact]
  public async Task CreateContact_Invalid_Email()
  {
    //Arange 
    var request = new ContactCreationDTO("Marie", "Marie", 31, "99999999");

    //Act
    var actual = await contactService.CreateContactValidation(request);

    //Assert
    Assert.Equal("E-mail inv�lido.", actual.Errors.First().Message);
    Assert.Equal("BadRequest", actual.StatusCode.ToString());
  }

  [Fact]
  public async Task CreateContact_Invalid_Name()
  {
    //Arange 
    var request = new ContactCreationDTO("Hi", "test@gmail.com", 31, "99999999");

    //Act
    var actual = await contactService.CreateContactValidation(request);

    //Assert
    Assert.Equal("Nome deve ter no minimo 3 caracteres.", actual.Errors.First().Message);
    Assert.Equal("BadRequest", actual.StatusCode.ToString());
  }
}

