using CampusEventHubApi.Controllers;
using CampusEventHubApi.DTOs;
using CampusEventHubApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusEventHubApi.Tests.Controllers
{
    [TestFixture]
    public class KalendarControllerTests
    {
        private Mock<IKalendarService> _kalendarServiceMock;
        private KalendarController _controller;

        [SetUp]
        public void SetUp()
        {
            _kalendarServiceMock = new Mock<IKalendarService>();
            _controller = new KalendarController(_kalendarServiceMock.Object);
        }

        [Test]
        public async Task GetKalendari_ReturnsOkObjectResult_WithListOfKalendari()
        {
            // Arrange
            var kalendari = new List<KalendarRequestDto>
            {
                new KalendarRequestDto { Naslov = "Event 1" },
                new KalendarRequestDto { Naslov = "Event 2" }
            };
            _kalendarServiceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(kalendari);

            // Act
            var result = await _controller.GetKalendari();

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(kalendari));
        }

        [Test]
        public async Task GetKalendar_ReturnsOkObjectResult_WhenKalendarExists()
        {
            // Arrange
            var kalendar = new KalendarRequestDto { Naslov = "Event 1" };
            _kalendarServiceMock.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(kalendar);

            // Act
            var result = await _controller.GetKalendar(1);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(kalendar));
        }

        [Test]
        public async Task GetKalendar_ReturnsNotFound_WhenKalendarDoesNotExist()
        {
            // Arrange
            _kalendarServiceMock.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((KalendarRequestDto)null);

            // Act
            var result = await _controller.GetKalendar(1);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task CreateKalendar_ReturnsCreatedAtActionResult_WhenKalendarIsCreated()
        {
            // Arrange
            var kalendar = new KalendarRequestDto { Naslov = "Event 1" };
            _kalendarServiceMock.Setup(s => s.CreateAsync(It.IsAny<KalendarRequestDto>())).ReturnsAsync(kalendar);

            // Act
            var result = await _controller.CreateKalendar(kalendar);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            var createdResult = result.Result as CreatedAtActionResult;
            Assert.That(createdResult.Value, Is.EqualTo(kalendar));
        }

        [Test]
        public async Task CreateKalendar_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Error", "Invalid model");

            // Act
            var result = await _controller.CreateKalendar(new KalendarRequestDto());

            // Assert
            Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task UpdateKalendar_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            _kalendarServiceMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<KalendarRequestDto>())).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateKalendar(1, new KalendarRequestDto());

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public async Task UpdateKalendar_ReturnsNotFound_WhenKalendarDoesNotExist()
        {
            // Arrange
            _kalendarServiceMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<KalendarRequestDto>())).ReturnsAsync(false);

            // Act
            var result = await _controller.UpdateKalendar(1, new KalendarRequestDto());

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task DeleteKalendar_ReturnsNoContent_WhenDeletionIsSuccessful()
        {
            // Arrange
            _kalendarServiceMock.Setup(s => s.DeleteAsync(It.IsAny<int>())).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteKalendar(1);

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public async Task DeleteKalendar_ReturnsNotFound_WhenKalendarDoesNotExist()
        {
            // Arrange
            _kalendarServiceMock.Setup(s => s.DeleteAsync(It.IsAny<int>())).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteKalendar(1);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }
    }
}