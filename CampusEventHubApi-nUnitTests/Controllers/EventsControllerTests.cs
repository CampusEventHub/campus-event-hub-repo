using CampusEventHubApi.Controllers;
using CampusEventHubApi.DTOs;
using CampusEventHubApi.Models;
using CampusEventHubApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusEventHubApi.Tests.Controllers
{
    [TestFixture]
    public class EventsControllerTests
    {
        private Mock<IEventManagementService> _mockEventService;
        private EventsController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockEventService = new Mock<IEventManagementService>();
            _controller = new EventsController(_mockEventService.Object);
        }

        #region GetEvents Tests

        [Test]
        public async Task GetEvents_ShouldReturnOkWithEventList_WhenEventsExist()
        {
            // Arrange
            var events = new List<Event>
            {
                new Event { IDEvent = 1, Title = "Event 1" },
                new Event { IDEvent = 2, Title = "Event 2" }
            };

            _mockEventService
                .Setup(service => service.GetAllEventsAsync())
                .ReturnsAsync(events);

            // Act
            var result = await _controller.GetEvents();

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(events));
        }

        #endregion

        #region GetEvent Tests

        [Test]
        public async Task GetEvent_ShouldReturnOkWithEvent_WhenEventExists()
        {
            // Arrange
            var eventItem = new Event { IDEvent = 1, Title = "Event 1" };

            _mockEventService
                .Setup(service => service.GetEventByIdAsync(1))
                .ReturnsAsync(eventItem);

            // Act
            var result = await _controller.GetEvent(1);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(eventItem));
        }

        [Test]
        public async Task GetEvent_ShouldReturnNotFound_WhenEventDoesNotExist()
        {
            // Arrange
            _mockEventService
                .Setup(service => service.GetEventByIdAsync(1))
                .ReturnsAsync((Event)null);

            // Act
            var result = await _controller.GetEvent(1);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }

        #endregion

        #region CreateEvent Tests

        [Test]
        public async Task CreateEvent_ShouldReturnCreatedAtAction_WhenEventIsCreatedSuccessfully()
        {
            // Arrange
            var eventRequest = new EventRequest
            {
                Title = "New Event",
                Description = "Event Description",
                Location = "Event Location",
                StartDate = System.DateTime.Now,
                EndDate = System.DateTime.Now.AddHours(2),
                UserID = 1
            };

            _mockEventService
                .Setup(service => service.CreateEventAsync(It.IsAny<Event>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.CreateEvent(eventRequest);

            // Assert
            Assert.That(result, Is.InstanceOf<CreatedAtActionResult>());
            var createdResult = result as CreatedAtActionResult;
            Assert.That(createdResult.ActionName, Is.EqualTo(nameof(_controller.CreateEvent)));
        }

        [Test]
        public async Task CreateEvent_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Title", "Title is required");

            var eventRequest = new EventRequest();

            // Act
            var result = await _controller.CreateEvent(eventRequest);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task CreateEvent_ShouldReturnBadRequest_WhenUserIsNotFound()
        {
            // Arrange
            var eventRequest = new EventRequest
            {
                Title = "New Event",
                Description = "Event Description",
                Location = "Event Location",
                StartDate = System.DateTime.Now,
                EndDate = System.DateTime.Now.AddHours(2),
                UserID = 1
            };

            _mockEventService
                .Setup(service => service.CreateEventAsync(It.IsAny<Event>()))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.CreateEvent(eventRequest);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult.Value, Is.EqualTo("User not found."));
        }

        #endregion

        #region UpdateEvent Tests

        [Test]
        public async Task UpdateEvent_ShouldReturnNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var eventRequest = new EventRequest
            {
                Title = "Updated Event",
                Description = "Updated Description",
                Location = "Updated Location",
                StartDate = System.DateTime.Now,
                EndDate = System.DateTime.Now.AddHours(2),
                UserID = 1
            };

            _mockEventService
                .Setup(service => service.UpdateEventAsync(1, It.IsAny<Event>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateEvent(1, eventRequest);

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public async Task UpdateEvent_ShouldReturnNotFound_WhenEventDoesNotExist()
        {
            // Arrange
            var eventRequest = new EventRequest
            {
                Title = "Updated Event",
                Description = "Updated Description",
                Location = "Updated Location",
                StartDate = System.DateTime.Now,
                EndDate = System.DateTime.Now.AddHours(2),
                UserID = 1
            };

            _mockEventService
                .Setup(service => service.UpdateEventAsync(1, It.IsAny<Event>()))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.UpdateEvent(1, eventRequest);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task UpdateEvent_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Title", "Title is required");

            var eventRequest = new EventRequest();

            // Act
            var result = await _controller.UpdateEvent(1, eventRequest);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        #endregion

        #region DeleteEvent Tests

        [Test]
        public async Task DeleteEvent_ShouldReturnNoContent_WhenDeleteIsSuccessful()
        {
            // Arrange
            _mockEventService
                .Setup(service => service.DeleteEventAsync(1))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteEvent(1);

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public async Task DeleteEvent_ShouldReturnNotFound_WhenEventDoesNotExist()
        {
            // Arrange
            _mockEventService
                .Setup(service => service.DeleteEventAsync(1))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteEvent(1);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        #endregion
    }
}