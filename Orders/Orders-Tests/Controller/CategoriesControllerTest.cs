using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using Orders_Backend.Controllers;
using Orders_Backend.UnitOfWork.Interfaces;
using Orders_Shared.DTOs;
using Orders_Shared.Entities;
using Orders_Shared.Responses;

namespace Orders_Tests.Controller
{
    [TestClass]
    public class CategoriesControllerTest
    {
        private Mock<IGenericUnitOfWork<Category>> _mockGenericUnitOfWork = null!;
        private Mock<ICategoriesUnitOfWork> _mockCategoriesUnitOfWork = null!;
        private CategoriesController _controller = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockGenericUnitOfWork = new Mock<IGenericUnitOfWork<Category>>();
            _mockCategoriesUnitOfWork = new Mock<ICategoriesUnitOfWork>();
            _controller = new CategoriesController(_mockGenericUnitOfWork.Object, _mockCategoriesUnitOfWork.Object);
        }

        [TestMethod]
        public async Task GetComboAsync_ShouldReturnOkResult_WhenCategoriesExist()
        {
            // Arrange
            var categories = new List<Category> { new() };                         
            _mockCategoriesUnitOfWork.Setup(x => x.GetComboAsync()).ReturnsAsync(categories);
            
            // Act
            var result = await _controller.GetComboAsync();
            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(categories, okResult!.Value);
            _mockCategoriesUnitOfWork.Verify(x => x.GetComboAsync(), Times.Once);
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnOkResult_WhenWassSuccessIsTrue()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var response = new ActionResponse<IEnumerable<Category>>
            {
                WasSuccess = true,
            };
            _mockCategoriesUnitOfWork.Setup(x => x.GetAsync(pagination)).ReturnsAsync(response);
            // Act
            var result = await _controller.GetAsync(pagination);
            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(response.Result, okResult!.Value);
            _mockCategoriesUnitOfWork.Verify(x => x.GetAsync(pagination), Times.Once);
        }

        [TestMethod]
        public async Task GetTaskAsync_ShouldReturnBadRequest_WhenWassSuccessIsFalse()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var response = new ActionResponse<IEnumerable<Category>>
            {
                WasSuccess = false,
            };
            _mockCategoriesUnitOfWork.Setup(x => x.GetAsync(pagination)).ReturnsAsync(response);
            // Act
            var result = await _controller.GetAsync(pagination);
            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockCategoriesUnitOfWork.Verify(x => x.GetAsync(pagination), Times.Once);
        }

        [TestMethod]
        public async Task GetTotalPagesAsync_ShouldReturnOkResult_WhenWassSuccessIsTrue()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var response = new ActionResponse<int>
            {
                WasSuccess = true,
                Result = 5
            };
            _mockCategoriesUnitOfWork.Setup(x => x.GetTotalPagesAsync(pagination)).ReturnsAsync(response);
            // Act
            var result = await _controller.GetTotalPagesAsync(pagination);
            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(response.Result, okResult!.Value);
            _mockCategoriesUnitOfWork.Verify(x => x.GetTotalPagesAsync(pagination), Times.Once);
        }

        [TestMethod]
        public async Task GetTotalPagesAsync_ShouldReturnBadRequest_WhenWassSuccessIsFalse()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var response = new ActionResponse<int>
            {
                WasSuccess = false,
            };
            _mockCategoriesUnitOfWork.Setup(x => x.GetTotalPagesAsync(pagination)).ReturnsAsync(response);
            // Act
            var result = await _controller.GetTotalPagesAsync(pagination);
            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockCategoriesUnitOfWork.Verify(x => x.GetTotalPagesAsync(pagination), Times.Once);
        }
    }
}