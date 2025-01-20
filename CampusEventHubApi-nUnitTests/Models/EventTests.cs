using CampusEventHubApi.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CampusEventHubApi.Tests.Models
{
    [TestFixture]
    public class EventTests
    {
        [Test]
        public void Event_ValidModel_IsValid()
        {
            // Arrange
            var eventModel = new Event
            {
                Title = "Sample Event",
                Description = "Description of the event",
                Location = "Sample Location",
                StartDate = DateTime.Now.AddDays(1),  // Start date in the future
                EndDate = DateTime.Now.AddDays(2),    // End date later than start date
                UserID = 1
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(eventModel, new ValidationContext(eventModel), validationResults, true);

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        public void Event_TitleIsRequired_ReturnsValidationError()
        {
            // Arrange
            var eventModel = new Event
            {
                Title = "",  // Empty title
                Description = "Description of the event",
                Location = "Sample Location",
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(2),
                UserID = 1
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(eventModel, new ValidationContext(eventModel), validationResults, true);

            // Assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults.Count, Is.EqualTo(1));
            Assert.That(validationResults[0].ErrorMessage, Is.EqualTo("The Title field is required."));
        }

        [Test]
        public void Event_TitleMaxLength_ReturnsValidationError()
        {
            // Arrange
            var eventModel = new Event
            {
                Title = new string('A', 256),  // Title longer than the max length
                Description = "Description of the event",
                Location = "Sample Location",
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(2),
                UserID = 1
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(eventModel, new ValidationContext(eventModel), validationResults, true);

            // Assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults.Count, Is.EqualTo(1));
            Assert.That(validationResults[0].ErrorMessage, Is.EqualTo("The field Title must be a string or array type with a maximum length of '255'."));
        }

        [Test]
        public void Event_DefaultValues_AreSetCorrectly()
        {
            // Arrange
            var eventModel = new Event
            {
                Title = "Sample Event",
                Description = "Description of the event",
                Location = "Sample Location",
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(2),
                UserID = 1
            };

            // Act & Assert
            Assert.That(eventModel.Approved, Is.False);  // Default value should be false
            Assert.That(eventModel.CreatedAt.Date, Is.EqualTo(DateTime.UtcNow.Date).Within(1).Days);  // CreatedAt should be current date (allowing a slight difference in time)
            Assert.That(eventModel.ImageUrl, Is.EqualTo("https://via.placeholder.com/150"));  // Default ImageUrl
        }
    }
}