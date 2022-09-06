using Microsoft.AspNetCore.Mvc;
using Moq;
using ToDoList.Controllers;
using ToDoList.Data;
using ToDoList.Data.Repositories;
using ToDoList.Model;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ToDoList.NUnitTest
{
    public class UserControllerTest
    {
        private readonly UserController _userController;
        private readonly Mock<IUserRepository> _iUsertRepository;

        public UserControllerTest()
        {
            
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            _iUsertRepository = new Mock<IUserRepository>();
            _userController = new UserController(configuration, _iUsertRepository.Object);
            
        }

        [Test]
        public async Task Should_register_user_OK()
        {
            //Arrange
            _iUsertRepository.Setup(x => x.RegisterUser(It.IsAny<User>()));

            //Act
            var expected = await _userController.RegisterUser(new User { Username = "MatiasL", Password = "Matias123", Created = new DateTime(2022, 08, 29), Email = "matiaslosada@saltpay.com" });

            //Assert
            Assert.IsAssignableFrom<CreatedResult>(expected);
        }

        [Test]
        public void Should_register_user_not_OK()
        {
            //Arrange
            _iUsertRepository.Setup(x => x.RegisterUser(It.IsAny<User>()))
                .Throws<Exception>();

            //Act


            //Assert
            Assert.ThrowsAsync<Exception>(async () => await _userController.RegisterUser(new User { Username = "MatiasL", Password = "Matias123", Created = new DateTime(2022, 08, 29), Email = "matiaslosada@saltpay.com" }));
        }

        [Test]
        public async Task Should_get_badRequest_null_user()
        {
            //Arrange
            _iUsertRepository.Setup(x => x.RegisterUser(It.IsAny<User>()));

            //Act
            var expected = await _userController.RegisterUser(new User {});

            //Assert
            Assert.IsAssignableFrom<BadRequestResult>(expected);
        }

        [Test]
        public async Task Should_get_badRequest_modelState_invalid_registerUser()
        {
            //Arrange
            _iUsertRepository.Setup(x => x.RegisterUser(It.IsAny<User>()));
            _userController.ModelState.AddModelError("fakeError", "fakeError");

            //Act
            var expected = await _userController.RegisterUser(new User { Username = "MatiasL" });

            //Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(expected);
        }

        [Test]
        public async Task Should_login_OK()
        {
            //Arrange
            _iUsertRepository.Setup(x => x.LoginUser(It.IsAny<User>()));

            //Act
            var expected = await _userController.LoginUser(new User { Username = "MatiasL", Password = "Matias123", Created = new DateTime(2022, 08, 29), Email = "matiaslosada@saltpay.com" });

            //Assert
            Assert.IsAssignableFrom<OkObjectResult>(expected);
        }

        [Test]
        public void Should_login_not_OK()
        {
            //Arrange
            _iUsertRepository.Setup(x => x.LoginUser(It.IsAny<User>()))
                .Throws<Exception>();

            //Act


            //Assert
            Assert.ThrowsAsync<Exception>(async () => await _userController.LoginUser(new User { Username = "MatiasL", Password = "Matias123", Created = new DateTime(2022, 08, 29), Email = "matiaslosada@saltpay.com" }));
        }

        [Test]
        public async Task Should_login_null_not_OK()
        {
            //Arrange
            _iUsertRepository.Setup(x => x.LoginUser(It.IsAny<User>()));

            //Act
            var expected = await _userController.LoginUser(new User { });

            //Assert
            Assert.IsAssignableFrom<BadRequestResult>(expected);
        }

        [Test]
        public async Task Should_get_badRequest_modelState_invalid_login()
        {
            //Arrange
            _iUsertRepository.Setup(x => x.LoginUser(It.IsAny<User>()));
            _userController.ModelState.AddModelError("fakeError", "fakeError");

            //Act
            var expected = await _userController.LoginUser(new User { Username = "MatiasL" });

            //Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(expected);
        }
    }
}