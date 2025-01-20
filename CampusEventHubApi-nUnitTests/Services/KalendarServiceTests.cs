using CampusEventHubApi.Data;
using CampusEventHubApi.DTOs;
using CampusEventHubApi.Models;
using CampusEventHubApi.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampusEventHubApi.Tests.Services
{
    [TestFixture]
    public class KalendarServiceTests
    {
        private ApplicationDbContext _context;
        private KalendarService _kalendarService;

        [SetUp]
        public void SetUp()
        {
            // Setup in-memory database for testing
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestKalendarDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _kalendarService = new KalendarService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            // Dispose the context after each test
            _context.Dispose();
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnCalendar_WhenIdExists()
        {
            // Arrange
            var calendar = new Kalendar { Naslov = "Test Event", Opis = "Test Description", Datum = DateTime.UtcNow };
            _context.Kalendari.Add(calendar);
            await _context.SaveChangesAsync();

            // Act
            var result = await _kalendarService.GetByIdAsync(calendar.KalendarID);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Naslov, Is.EqualTo("Test Event"));
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnNull_WhenIdDoesNotExist()
        {
            // Act
            var result = await _kalendarService.GetByIdAsync(999);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task CreateAsync_ShouldCreateCalendar_WhenValidData()
        {
            // Arrange
            var request = new KalendarRequestDto
            {
                Naslov = "New Event",
                Opis = "Event Description",
                Datum = DateTime.UtcNow.ToString("dd.MM.yyyy"),
                Vrijeme = "14:00"
            };

            // Act
            var result = await _kalendarService.CreateAsync(request);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Naslov, Is.EqualTo("New Event"));
        }

        [Test]
        public void CreateAsync_ShouldThrowFormatException_WhenInvalidDateFormat()
        {
            // Arrange
            var request = new KalendarRequestDto
            {
                Naslov = "Invalid Event",
                Opis = "Invalid Date Format",
                Datum = "InvalidDate",
                Vrijeme = "14:00"
            };

            // Act & Assert
            Assert.ThrowsAsync<FormatException>(async () => await _kalendarService.CreateAsync(request));
        }

        [Test]
        public async Task UpdateAsync_ShouldUpdateCalendar_WhenValidData()
        {
            // Arrange
            var calendar = new Kalendar { Naslov = "Test Event", Opis = "Test Description", Datum = DateTime.UtcNow };
            _context.Kalendari.Add(calendar);
            await _context.SaveChangesAsync();

            var request = new KalendarRequestDto
            {
                Naslov = "Updated Event",
                Opis = "Updated Description",
                Datum = DateTime.UtcNow.ToString("dd.MM.yyyy"),
                Vrijeme = "16:00"
            };

            // Act
            var result = await _kalendarService.UpdateAsync(calendar.KalendarID, request);

            // Assert
            Assert.That(result, Is.True);
            var updatedCalendar = await _context.Kalendari.FindAsync(calendar.KalendarID);
            Assert.That(updatedCalendar.Naslov, Is.EqualTo("Updated Event"));
        }

        [Test]
        public async Task UpdateAsync_ShouldReturnFalse_WhenCalendarNotFound()
        {
            // Arrange
            var request = new KalendarRequestDto
            {
                Naslov = "Non-Existent Event",
                Opis = "This event does not exist",
                Datum = DateTime.UtcNow.ToString("dd.MM.yyyy"),
                Vrijeme = "18:00"
            };

            // Act
            var result = await _kalendarService.UpdateAsync(999, request);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task DeleteAsync_ShouldDeleteCalendar_WhenExists()
        {
            // Arrange
            var calendar = new Kalendar { Naslov = "Event to Delete", Opis = "Description", Datum = DateTime.UtcNow };
            _context.Kalendari.Add(calendar);
            await _context.SaveChangesAsync();

            // Act
            var result = await _kalendarService.DeleteAsync(calendar.KalendarID);

            // Assert
            Assert.That(result, Is.True);
            var deletedCalendar = await _context.Kalendari.FindAsync(calendar.KalendarID);
            Assert.That(deletedCalendar, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_ShouldReturnFalse_WhenCalendarNotFound()
        {
            // Act
            var result = await _kalendarService.DeleteAsync(999);

            // Assert
            Assert.That(result, Is.False);
        }
    }
}