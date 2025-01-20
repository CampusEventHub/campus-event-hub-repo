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
    public class UserControllerTests
    {
        private Mock<IUserManagementService> _mockUserService;
        private UserController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockUserService = new Mock<IUserManagementService>();
            _controller = new UserController(_mockUserService.Object);
        }

        [Test]
        public async Task GetUserById_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            int nonExistentId = 999;
            _mockUserService.Setup(service => service.GetUserByIdAsync(nonExistentId)).ReturnsAsync((User)null);

            // Act
            var result = await _controller.GetUserById(nonExistentId);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            Assert.That(notFoundResult, Is.Not.Null);
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task UpdateUser_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            int nonExistentId = 999;
            var updateRequest = new UserUpdateRequest { Username = "newuser", Email = "newuser@example.com" };
            _mockUserService.Setup(service => service.GetUserByIdAsync(nonExistentId)).ReturnsAsync((User)null);

            // Act
            var result = await _controller.UpdateUser(nonExistentId, updateRequest);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            Assert.That(notFoundResult, Is.Not.Null);
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task DeleteUser_ReturnsOkResult_WhenDeletedSuccessfully()
        {
            // Arrange
            int id = 1;
            _mockUserService.Setup(service => service.DeleteUserAsync(id)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteUser(id);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.EqualTo("User deleted successfully."));
        }

        [Test]
        public async Task DeleteUser_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            int nonExistentId = 999;
            _mockUserService.Setup(service => service.DeleteUserAsync(nonExistentId)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteUser(nonExistentId);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            Assert.That(notFoundResult, Is.Not.Null);
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
        }
    }
}