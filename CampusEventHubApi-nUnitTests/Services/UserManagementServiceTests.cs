using CampusEventHubApi.Data;
using CampusEventHubApi.Models;
using CampusEventHubApi.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CampusEventHubApi.Tests.Services
{
    [TestFixture]
    public class UserManagementServiceTests : IDisposable
    {
        private ApplicationDbContext _context;
        private UserManagementService _userManagementService;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test run
                .Options;

            _context = new ApplicationDbContext(options);
            _userManagementService = new UserManagementService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            // Ensure the context is disposed after each test
            _context.Dispose();
        }

        public void Dispose()
        {
            // Dispose of any resources that are not already disposed
            _context?.Dispose();
        }

        [Test]
        public async Task GetUsersAsync_ShouldReturnAllUsers()
        {
            // Arrange
            var user1 = new User { Username = "User 1", Email = "user1@example.com", PasswordHash = "hashedpassword", Salt = "salt" };
            var user2 = new User { Username = "User 2", Email = "user2@example.com", PasswordHash = "hashedpassword", Salt = "salt" };

            _context.User.Add(user1);
            _context.User.Add(user2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _userManagementService.GetUsersAsync();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetUserByIdAsync_ShouldReturnCorrectUser()
        {
            // Arrange
            var user = new User { Username = "User 1", Email = "user1@example.com", PasswordHash = "hashedpassword", Salt = "salt" };
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _userManagementService.GetUserByIdAsync(user.IDUser);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Username, Is.EqualTo(user.Username));
        }

        [Test]
        public async Task UpdateUserAsync_ShouldUpdateUser()
        {
            // Arrange
            var user = new User { Username = "User 1", Email = "user1@example.com", PasswordHash = "hashedpassword", Salt = "salt" };
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            var updatedUser = new User { Username = "Updated User", Email = "updated@example.com", PasswordHash = "newhashedpassword", Salt = "newsalt" };

            // Act
            var result = await _userManagementService.UpdateUserAsync(user.IDUser, updatedUser);

            // Assert
            Assert.That(result, Is.True);
            var updatedUserFromDb = await _context.User.FindAsync(user.IDUser);
            Assert.That(updatedUserFromDb.Username, Is.EqualTo(updatedUser.Username));
            Assert.That(updatedUserFromDb.Email, Is.EqualTo(updatedUser.Email));
        }

        [Test]
        public async Task DeleteUserAsync_ShouldDeleteUser()
        {
            // Arrange
            var user = new User { Username = "User 1", Email = "user1@example.com", PasswordHash = "hashedpassword", Salt = "salt" };
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _userManagementService.DeleteUserAsync(user.IDUser);

            // Assert
            Assert.That(result, Is.True);
            var deletedUser = await _context.User.FindAsync(user.IDUser);
            Assert.That(deletedUser, Is.Null);
        }

        [Test]
        public async Task ChangePasswordAsync_ShouldFailWithWrongOldPassword()
        {
            // Arrange
            var user = new User { Username = "User 1", Email = "user1@example.com", PasswordHash = "hashedpassword", Salt = "salt" };
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _userManagementService.ChangePasswordAsync(user.IDUser, "wrongpassword", "newpassword");

            // Assert
            Assert.That(result, Is.False);
        }
    }
}