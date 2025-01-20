using CampusEventHubApi.Controllers;
using CampusEventHubApi.DTOs;
using CampusEventHubApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusEventHubApi.Tests.Controllers
{
    [TestFixture]
    public class OcjeneControllerTests
    {
        private Mock<IOcjenaService> _mockOcjenaService;
        private OcjeneController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockOcjenaService = new Mock<IOcjenaService>();
            _controller = new OcjeneController(_mockOcjenaService.Object);
        }

        [Test]
        public async Task GetOcjene_ReturnsOkResult_WhenRatingsExist()
        {
            // Arrange
            var mockRatings = new List<OcjenaResponseDto>
            {
                new OcjenaResponseDto { OcjenaID = 1, OcjenaVrijednost = 5 },
                new OcjenaResponseDto { OcjenaID = 2, OcjenaVrijednost = 4 }
            };
            _mockOcjenaService.Setup(service => service.GetAllAsync()).ReturnsAsync(mockRatings);

            // Act
            var result = await _controller.GetOcjene();

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.EqualTo(mockRatings));
        }

        [Test]
        public async Task GetOcjena_ReturnsNotFound_WhenRatingDoesNotExist()
        {
            // Arrange
            int nonExistentId = 999;
            _mockOcjenaService.Setup(service => service.GetByIdAsync(nonExistentId)).ReturnsAsync((OcjenaResponseDto)null);

            // Act
            var result = await _controller.GetOcjena(nonExistentId);

            // Assert
            var notFoundResult = result.Result as NotFoundObjectResult;
            Assert.That(notFoundResult, Is.Not.Null);
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task CreateOcjena_ReturnsCreatedAtAction_WhenValid()
        {
            // Arrange
            var request = new OcjenaRequestDto { OcjenaVrijednost = 5 };
            var createdRating = new OcjenaResponseDto { OcjenaID = 1, OcjenaVrijednost = 5 };
            _mockOcjenaService.Setup(service => service.CreateAsync(request)).ReturnsAsync(createdRating);

            // Act
            var result = await _controller.CreateOcjena(request);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.That(createdAtActionResult, Is.Not.Null);
            Assert.That(createdAtActionResult.StatusCode, Is.EqualTo(201));
            Assert.That(createdAtActionResult.Value, Is.EqualTo(createdRating));
        }

        [Test]
        public async Task UpdateOcjena_ReturnsNoContent_WhenUpdatedSuccessfully()
        {
            // Arrange
            int id = 1;
            var updateRequest = new OcjenaUpdateDto { OcjenaVrijednost = 4 };
            _mockOcjenaService.Setup(service => service.UpdateAsync(id, updateRequest)).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateOcjena(id, updateRequest);

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public async Task DeleteOcjena_ReturnsNotFound_WhenRatingDoesNotExist()
        {
            // Arrange
            int nonExistentId = 999;
            _mockOcjenaService.Setup(service => service.DeleteAsync(nonExistentId)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteOcjena(nonExistentId);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            Assert.That(notFoundResult, Is.Not.Null);
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task DeleteOcjena_ReturnsNoContent_WhenDeletedSuccessfully()
        {
            // Arrange
            int id = 1;
            _mockOcjenaService.Setup(service => service.DeleteAsync(id)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteOcjena(id);

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }
    }
}