using Microsoft.AspNetCore.Mvc;
using Moq;
using ToDoList.Controllers;
using ToDoList.Data;
using ToDoList.Data.Repositories;
using ToDoList.Model;
using System.Threading.Tasks;

namespace ToDoList.NUnitTest
{
    public class ListControllerTest
    {
        private readonly ListController _listController;
        private readonly Mock<IListRepository> _iListRepository;   
       
        public ListControllerTest()
            
        {
            _iListRepository = new Mock<IListRepository>();
            _listController = new ListController(_iListRepository.Object);
        }

        [Test]
        public async Task Should_get_all_items_OK()
        {
            //Arrange
            _iListRepository.Setup(x => x.GetAllItems());

            //Act
            var expected = await _listController.GetAllItems();

            //Assert
            Assert.IsAssignableFrom<OkObjectResult>(expected);
        }

        [Test]
        public void Should_get_all_items_Not_OK()
        {
            //Arrange
            _iListRepository.Setup(x => x.GetAllItems())
                .Throws<Exception>();

            //Act


            //Assert
            Assert.ThrowsAsync<Exception>(async () => await _listController.GetAllItems());
        }

        [Test]
        public async Task Should_get_item_by_ID_OK()
        {
            //Arrange
            _iListRepository.Setup(x => x.GetItemDetails(It.IsAny<int>()));

            //Act
            var expected = await _listController.GetItemDetails(2);

            //Assert
            Assert.IsAssignableFrom<OkObjectResult>(expected);
        }

        [Test]
        public void Should_get_item_by_ID_Not_OK()
        {
            //Arrange
            _iListRepository.Setup(x => x.GetItemDetails(It.IsAny<int>()))
                .Throws<Exception>();

            //Act


            //Assert
            Assert.ThrowsAsync<Exception>(async () => await _listController.GetItemDetails(2));
        }

        [Test]
        public async Task Should_Insert_Item_OK()
        {
            //Arrange
            _iListRepository.Setup(x => x.InsertItem(It.IsAny<AList>()));

            //Act
            var expected = await _listController.InsertItem(new AList { Id = 1, Title = "Unitest", Description = "Complete all unitest", DueDate = new DateTime(2022, 09, 29), Completion_Date = new DateTime(2022, 09, 29), Created_By = "Matias", Status = "todo", SentMail = false, User = "admin" });

            //Assert
            Assert.IsAssignableFrom<CreatedResult>(expected);
        }

        [Test]
        public void Should_Insert_Item_Not_OK()
        {
            //Arrange
            _iListRepository.Setup(x => x.InsertItem(It.IsAny<AList>()))
                .Throws<Exception>();

            //Act


            //Assert
            Assert.ThrowsAsync<Exception>(async () => await _listController.InsertItem(new AList { Id = 1, Title = "Unitest", Description = "Complete all unitest", DueDate = new DateTime(2022, 09, 29), Completion_Date = new DateTime(2022, 09, 29), Created_By = "Matias", Status = "todo", SentMail = false, User = "admin" }));
        }

        [Test]
        public async Task Should_Update_Item_OK()
        {
            //Arrange
            _iListRepository.Setup(x => x.UpdateItem(It.IsAny<AList>()));

            //Act
            var expected = await _listController.UpdateItem(new AList { Id = 1, Title = "Unitest", Description = "Complete all unitest", DueDate = new DateTime(2022, 09, 29), Completion_Date = new DateTime(2022, 09, 30), Created_By = "Matias", Status = "todo", SentMail = false, User = "admin" });

            //Assert
            Assert.IsAssignableFrom<NoContentResult>(expected);
        }

        [Test]
        public void Should_Update_Item_Not_OK()
        {
            //Arrange
            _iListRepository.Setup(x => x.UpdateItem(It.IsAny<AList>()))
                .Throws<Exception>();

            //Act


            //Assert
            Assert.ThrowsAsync<Exception>(async () => await _listController.UpdateItem(new AList { Id = 1, Title = "Unitest", Description = "Complete all unitest", DueDate = new DateTime(2022, 09, 29), Completion_Date = new DateTime(2022, 09, 30), Created_By = "Matias", Status = "todo", SentMail = false, User = "admin" }));
        }

        [Test]
        public async Task Should_Delete_Item_OK()
        {
            //Arrange
            _iListRepository.Setup(x => x.DeleteItem(It.IsAny<AList>()));

            //Act
            var expected = await _listController.DeleteItem(1);

            //Assert
            Assert.IsAssignableFrom<NoContentResult>(expected);
        }

        [Test]
        public void Should_Delete_Item_Not_OK()
        {
            //Arrange
            _iListRepository.Setup(x => x.DeleteItem(It.IsAny<AList>()))
                .Throws<Exception>();

            //Act


            //Assert
            Assert.ThrowsAsync<Exception>(async () => await _listController.DeleteItem(1));
        }
    }
}