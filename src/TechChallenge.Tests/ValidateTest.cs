using TechChallenge.Domain.Repository;

namespace TechChallenge.Tests
{
    public class ValidateTest
    {
        private readonly ContactService contactService;
        public ValidateTest()
        {
            var mockContactService = new Mock<IContactRepository>();
            contactService = new ContactService(mockContactService.Object);
        }

        [Theory]
        [InlineData("E9999999", "Informe somente números no telefone")]
        [InlineData("9999999", "Telefone tem que ter no minimo 8 números.")]
        [InlineData("9999999999", "Telefone tem que ter no máximo 9 números.")]
        public async Task CreateContact_Invalid_Phone(string phone, string error)
        {
            //Arange 
            var request = new ContactCreationDTO("Marie", "test@gmail.com", 31, phone);
 
            //Act
            var actual = await contactService.CreateContact(request);

            //Assert
            Assert.Equal(error, actual.Errors.First().Message);
            Assert.Equal("BadRequest", actual.StatusCode.ToString());
        }

        [Theory]
        [InlineData(09, "DDD está abaixo da faixa no Brasil.")]
        [InlineData(101, "DDD está acima da faixa no Brasil.")]
        public async Task CreateContact_Invalid_DDD(int ddd, string error)
        {
            //Arange 
            var request = new ContactCreationDTO("Marie", "test@gmail.com", ddd, "99999999");

            //Act
            var actual = await contactService.CreateContact(request);

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
            var actual = await contactService.CreateContact(request);

            //Assert
            Assert.Equal("E-mail inválido.", actual.Errors.First().Message);
            Assert.Equal("BadRequest", actual.StatusCode.ToString());
        }

        [Fact]
        public async Task CreateContact_Invalid_Name()
        {
            //Arange 
            var request = new ContactCreationDTO("Hi", "test@gmail.com", 31, "99999999");

            //Act
            var actual = await contactService.CreateContact(request);

            //Assert
            Assert.Equal("Nome deve ter no minimo 3 caracteres.", actual.Errors.First().Message);
            Assert.Equal("BadRequest", actual.StatusCode.ToString());
        }
    }
}
