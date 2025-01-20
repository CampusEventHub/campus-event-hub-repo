using CampusEventHubApi.Data;
using CampusEventHubApi.Models;
using CampusEventHubApi.Security;
using CampusEventHubApi.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace CampusEventHubApi.Tests.Services
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        private ApplicationDbContext _context;
        private AuthenticationService _authenticationService;

        [SetUp]
        public void SetUp()
        {
            // Setup in-memory database for testing
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _authenticationService = new AuthenticationService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            // Dispose the context after each test
            _context.Dispose();
        }

        [Test]
        public async Task RegisterUserAsync_ShouldReturnFalse_WhenUsernameOrEmailAlreadyExists()
        {
            // Arrange
            await _authenticationService.RegisterUserAsync("testuser", "test@example.com", "TestPassword123!");

            // Act
            var result = await _authenticationService.RegisterUserAsync("testuser", "otheremail@example.com", "AnotherPassword123!");

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task AuthenticateUserAsync_ShouldReturnUser_WhenCredentialsAreCorrect()
        {
            // Arrange
            await _authenticationService.RegisterUserAsync("testuser", "test@example.com", "TestPassword123!");

            // Act
            var user = await _authenticationService.AuthenticateUserAsync("testuser", "TestPassword123!");

            // Assert
            Assert.That(user, Is.Not.Null);
            Assert.That(user.Username, Is.EqualTo("testuser"));
        }

        [Test]
        public async Task AuthenticateUserAsync_ShouldReturnNull_WhenCredentialsAreIncorrect()
        {
            // Arrange
            await _authenticationService.RegisterUserAsync("testuser", "test@example.com", "TestPassword123!");

            // Act
            var user = await _authenticationService.AuthenticateUserAsync("testuser", "WrongPassword123!");

            // Assert
            Assert.That(user, Is.Null);
        }

        [Test]
        public async Task AuthenticateUserAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Act
            var user = await _authenticationService.AuthenticateUserAsync("nonexistentuser", "SomePassword123!");

            // Assert
            Assert.That(user, Is.Null);
        }
    }
}