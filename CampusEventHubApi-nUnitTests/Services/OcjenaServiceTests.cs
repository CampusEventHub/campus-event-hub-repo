using CampusEventHubApi.Data;
using CampusEventHubApi.DTOs;
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
    public class OcjenaServiceTests
    {
        private ApplicationDbContext _context;
        private OcjenaService _ocjenaService;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test run
                .Options;

            _context = new ApplicationDbContext(options);

            // Ensure the context is not null
            if (_context == null)
            {
                throw new ArgumentNullException(nameof(_context), "DbContext cannot be null");
            }

            _ocjenaService = new OcjenaService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            // Dispose of the DbContext after each test to avoid NUnit103 error
            _context?.Dispose();
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllOcjene()
        {
            // Arrange
            var event1 = new Event
            {
                IDEvent = 1,
                Title = "Event 1",
                Description = "Event description",
                Location = "Event location"
            };

            var user1 = new User
            {
                IDUser = 1,
                Username = "User 1",
                Email = "user1@example.com",
                PasswordHash = "hashedpassword",
                Salt = "salt"
            };

            var ocjena1 = new Ocjena
            {
                EventID = 1,
                UserID = 1,
                OcjenaVrijednost = 5,
                Komentar = "Excellent"
            };

            var ocjena2 = new Ocjena
            {
                EventID = 1,
                UserID = 1,
                OcjenaVrijednost = 3,
                Komentar = "Good"
            };

            _context.Event.Add(event1);
            _context.User.Add(user1);
            _context.Ocjene.Add(ocjena1);
            _context.Ocjene.Add(ocjena2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _ocjenaService.GetAllAsync();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnCorrectOcjena()
        {
            // Arrange
            var event1 = new Event
            {
                IDEvent = 1,
                Title = "Event 1",
                Description = "Event description",
                Location = "Event location"
            };

            var user1 = new User
            {
                IDUser = 1,
                Username = "User 1",
                Email = "user1@example.com",
                PasswordHash = "hashedpassword",
                Salt = "salt"
            };

            var ocjena = new Ocjena
            {
                EventID = 1,
                UserID = 1,
                OcjenaVrijednost = 5,
                Komentar = "Excellent"
            };

            _context.Event.Add(event1);
            _context.User.Add(user1);
            _context.Ocjene.Add(ocjena);
            await _context.SaveChangesAsync();

            // Act
            var result = await _ocjenaService.GetByIdAsync(ocjena.OcjenaID);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.OcjenaID, Is.EqualTo(ocjena.OcjenaID));
        }

        [Test]
        public async Task CreateAsync_ShouldCreateNewOcjena()
        {
            // Arrange
            var event1 = new Event
            {
                IDEvent = 1,
                Title = "Event 1",
                Description = "Event description",
                Location = "Event location"
            };

            var user1 = new User
            {
                IDUser = 1,
                Username = "User 1",
                Email = "user1@example.com",
                PasswordHash = "hashedpassword",
                Salt = "salt"
            };

            _context.Event.Add(event1);
            _context.User.Add(user1);
            await _context.SaveChangesAsync();

            var request = new OcjenaRequestDto
            {
                EventID = 1,
                UserID = 1,
                OcjenaVrijednost = 5,
                Komentar = "Excellent"
            };

            // Act
            var result = await _ocjenaService.CreateAsync(request);

            // Assert
            var createdOcjena = await _context.Ocjene.FindAsync(result.OcjenaID);
            Assert.That(createdOcjena, Is.Not.Null);
            Assert.That(createdOcjena.OcjenaVrijednost, Is.EqualTo(request.OcjenaVrijednost));
        }

        [Test]
        public async Task UpdateAsync_ShouldUpdateExistingOcjena()
        {
            // Arrange
            var event1 = new Event
            {
                IDEvent = 1,
                Title = "Event 1",
                Description = "Event description",
                Location = "Event location"
            };

            var user1 = new User
            {
                IDUser = 1,
                Username = "User 1",
                Email = "user1@example.com",
                PasswordHash = "hashedpassword",
                Salt = "salt"
            };

            var ocjena = new Ocjena
            {
                EventID = 1,
                UserID = 1,
                OcjenaVrijednost = 5,
                Komentar = "Excellent"
            };

            _context.Event.Add(event1);
            _context.User.Add(user1);
            _context.Ocjene.Add(ocjena);
            await _context.SaveChangesAsync();

            var updateRequest = new OcjenaUpdateDto
            {
                OcjenaVrijednost = 4,
                Komentar = "Good"
            };

            // Act
            var result = await _ocjenaService.UpdateAsync(ocjena.OcjenaID, updateRequest);

            // Assert
            Assert.That(result, Is.True);
            var updatedOcjena = await _context.Ocjene.FindAsync(ocjena.OcjenaID);
            Assert.That(updatedOcjena.OcjenaVrijednost, Is.EqualTo(4));
            Assert.That(updatedOcjena.Komentar, Is.EqualTo("Good"));
        }

        [Test]
        public async Task DeleteAsync_ShouldDeleteOcjena()
        {
            // Arrange
            var event1 = new Event
            {
                IDEvent = 1,
                Title = "Event 1",
                Description = "Event description",
                Location = "Event location"
            };

            var user1 = new User
            {
                IDUser = 1,
                Username = "User 1",
                Email = "user1@example.com",
                PasswordHash = "hashedpassword",
                Salt = "salt"
            };

            var ocjena = new Ocjena
            {
                EventID = 1,
                UserID = 1,
                OcjenaVrijednost = 5,
                Komentar = "Excellent"
            };

            _context.Event.Add(event1);
            _context.User.Add(user1);
            _context.Ocjene.Add(ocjena);
            await _context.SaveChangesAsync();

            // Act
            var result = await _ocjenaService.DeleteAsync(ocjena.OcjenaID);

            // Assert
            Assert.That(result, Is.True);
            var deletedOcjena = await _context.Ocjene.FindAsync(ocjena.OcjenaID);
            Assert.That(deletedOcjena, Is.Null);
        }
    }
}