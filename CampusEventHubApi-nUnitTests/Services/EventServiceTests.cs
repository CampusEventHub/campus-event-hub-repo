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
    public class EventServiceTests
    {
        private ApplicationDbContext _context;
        private EventService _eventService;

        [SetUp]
        public void SetUp()
        {
            // Setup in-memory database for testing
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _eventService = new EventService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            // Dispose the context after each test
            _context.Dispose();
        }

        [Test]
        public async Task GetAllEventsAsync_ShouldReturnAllEvents()
        {
            // Arrange
            _context.Event.Add(new Event
            {
                Title = "Event 1",
                Description = "Test event 1",
                UserID = 1,
                Location = "Location 1" // Set the Location here
            });
            _context.Event.Add(new Event
            {
                Title = "Event 2",
                Description = "Test event 2",
                UserID = 2,
                Location = "Location 2" // Set the Location here
            });
            await _context.SaveChangesAsync();

            // Act
            var events = await _eventService.GetAllEventsAsync();

            // Assert
            Assert.That(events.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetEventByIdAsync_ShouldReturnEvent_WhenEventExists()
        {
            // Arrange
            var newEvent = new Event
            {
                Title = "Event 1",
                Description = "Test event 1",
                UserID = 1,
                Location = "Location 1" // Set the Location here
            };
            _context.Event.Add(newEvent);
            await _context.SaveChangesAsync();

            // Act
            var eventFromDb = await _eventService.GetEventByIdAsync(newEvent.IDEvent);

            // Assert
            Assert.That(eventFromDb, Is.Not.Null);
            Assert.That(eventFromDb.Title, Is.EqualTo("Event 1"));
        }

        [Test]
        public async Task CreateEventAsync_ShouldReturnTrue_WhenEventIsCreatedSuccessfully()
        {
            // Arrange
            var newEvent = new Event
            {
                Title = "New Event",
                Description = "Test event description",
                UserID = 1,
                Location = "Some Location" // Set the Location here
            };

            // Act
            var result = await _eventService.CreateEventAsync(newEvent);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(_context.Event.Any(e => e.Title == "New Event"), Is.True);
        }

        [Test]
        public async Task CreateEventAsync_ShouldReturnFalse_WhenUserDoesNotExist()
        {
            // Arrange
            var newEvent = new Event
            {
                Title = "New Event",
                Description = "Test event description",
                UserID = 9999, // Invalid UserID
                Location = "Some Location" // Set the Location here
            };

            // Act
            var result = await _eventService.CreateEventAsync(newEvent);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task UpdateEventAsync_ShouldReturnTrue_WhenEventIsUpdatedSuccessfully()
        {
            // Arrange
            var newEvent = new Event
            {
                Title = "Event to Update",
                Description = "Old description",
                UserID = 1,
                Location = "Old Location" // Set the Location here
            };
            _context.Event.Add(newEvent);
            await _context.SaveChangesAsync();

            // Act
            var updatedEvent = new Event
            {
                Title = "Updated Event",
                Description = "New description",
                UserID = 1,
                Location = "New Location" // Set the Location here
            };
            var result = await _eventService.UpdateEventAsync(newEvent.IDEvent, updatedEvent);

            // Assert
            Assert.That(result, Is.True);
            var eventFromDb = await _eventService.GetEventByIdAsync(newEvent.IDEvent);
            Assert.That(eventFromDb.Description, Is.EqualTo("New description"));
        }

        [Test]
        public async Task UpdateEventAsync_ShouldReturnFalse_WhenEventDoesNotExist()
        {
            // Arrange
            var updatedEvent = new Event
            {
                Title = "Non-existent Event",
                Description = "Description",
                UserID = 1,
                Location = "Non-existent Location" // Set the Location here
            };

            // Act
            var result = await _eventService.UpdateEventAsync(9999, updatedEvent); // Non-existent event ID

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task DeleteEventAsync_ShouldReturnTrue_WhenEventIsDeletedSuccessfully()
        {
            // Arrange
            var newEvent = new Event
            {
                Title = "Event to Delete",
                Description = "Test event description",
                UserID = 1,
                Location = "Location to Delete" // Set the Location here
            };
            _context.Event.Add(newEvent);
            await _context.SaveChangesAsync();

            // Act
            var result = await _eventService.DeleteEventAsync(newEvent.IDEvent);

            // Assert
            Assert.That(result, Is.True);
            var eventFromDb = await _eventService.GetEventByIdAsync(newEvent.IDEvent);
            Assert.That(eventFromDb, Is.Null);
        }

        [Test]
        public async Task DeleteEventAsync_ShouldReturnFalse_WhenEventDoesNotExist()
        {
            // Act
            var result = await _eventService.DeleteEventAsync(9999); // Non-existent event ID

            // Assert
            Assert.That(result, Is.False);
        }
    }
}