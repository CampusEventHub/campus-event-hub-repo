using CampusEventHubApi.Controllers;
using CampusEventHubApi.DTOs;
using CampusEventHubApi.Models;
using CampusEventHubApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CampusEventHubApi.Tests.Controllers
{
    [TestFixture]
    public class AuthControllerTests
    {
        private Mock<IAuthenticationService> _mockAuthService;
        private Mock<IUserManagementService> _mockUserService;
        private Mock<IConfiguration> _mockConfiguration;
        private AuthController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockAuthService = new Mock<IAuthenticationService>();
            _mockUserService = new Mock<IUserManagementService>();
            _mockConfiguration = new Mock<IConfiguration>();
            _controller = new AuthController(_mockAuthService.Object, _mockUserService.Object, _mockConfiguration.Object);
        }

        [Test]
        public async Task Register_ShouldReturnOk_WhenRegistrationIsSuccessful()
        {
            // Arrange
            var registrationRequest = new UserRegistrationRequest
            {
                Username = "testuser",
                Email = "testuser@example.com",
                Password = "password123"
            };

            _mockAuthService
                .Setup(x => x.RegisterUserAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.Register(registrationRequest);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo("User successfully registered."));
        }

        [Test]
        public async Task Register_ShouldReturnBadRequest_WhenRegistrationFails()
        {
            // Arrange
            var registrationRequest = new UserRegistrationRequest
            {
                Username = "testuser",
                Email = "testuser@example.com",
                Password = "password123"
            };

            _mockAuthService
                .Setup(x => x.RegisterUserAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.Register(registrationRequest);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult.Value, Is.EqualTo("Username or email already exists."));
        }


        [Test]
        public async Task Login_ShouldReturnUnauthorized_WhenAuthenticationFails()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                Username = "testuser",
                Password = "wrongpassword"
            };

            _mockAuthService
                .Setup(x => x.AuthenticateUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((User)null);

            // Act
            var result = await _controller.Login(loginRequest);

            // Assert
            Assert.That(result, Is.InstanceOf<UnauthorizedObjectResult>());
            var unauthorizedResult = result as UnauthorizedObjectResult;
            Assert.That(unauthorizedResult.Value, Is.EqualTo("Invalid username or password."));
        }


        [Test]
        public async Task ChangePassword_ShouldReturnBadRequest_WhenOldPasswordIsIncorrect()
        {
            // Arrange
            var changePasswordRequest = new ChangePasswordRequest
            {
                OldPassword = "wrongpassword",
                NewPassword = "newpassword123"
            };

            var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("IDUser", "1")
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = userClaims }
            };

            _mockUserService
                .Setup(x => x.ChangePasswordAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.ChangePassword(changePasswordRequest);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult.Value, Is.EqualTo("Old password is incorrect or user not found."));
        }

        [Test]
        public async Task ChangePassword_ShouldReturnUnauthorized_WhenUserIsNotAuthenticated()
        {
            // Arrange
            var changePasswordRequest = new ChangePasswordRequest
            {
                OldPassword = "oldpassword123",
                NewPassword = "newpassword123"
            };

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = null } // User is not authenticated
            };

            // Act
            var result = await _controller.ChangePassword(changePasswordRequest);

            // Assert
            Assert.That(result, Is.InstanceOf<UnauthorizedObjectResult>());
            var unauthorizedResult = result as UnauthorizedObjectResult;
            Assert.That(unauthorizedResult.Value, Is.EqualTo("User not authenticated."));
        }
    }
}