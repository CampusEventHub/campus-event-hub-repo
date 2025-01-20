using CampusEventHubApi.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CampusEventHubApi.Tests.Models
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void User_ValidModel_ReturnsValid()
        {
            // Arrange
            var user = new User
            {
                Username = "testuser",
                Email = "testuser@example.com",
                PasswordHash = "hashedpassword",
                Salt = "randomsalt"
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(user, new ValidationContext(user), validationResults, true);

            // Assert
            Assert.That(isValid, Is.True);  // Model should be valid
            Assert.That(validationResults.Count, Is.EqualTo(0));  // No validation errors
        }

        [Test]
        public void User_InvalidEmail_ReturnsValidationError()
        {
            // Arrange
            var user = new User
            {
                Username = "testuser",
                Email = "invalidemail",  // Invalid email format
                PasswordHash = "hashedpassword",
                Salt = "randomsalt"
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(user, new ValidationContext(user), validationResults, true);

            // Assert
            Assert.That(isValid, Is.False);  // Model should be invalid
            Assert.That(validationResults.Count, Is.EqualTo(1));  // One validation error for email
            Assert.That(validationResults[0].ErrorMessage, Is.EqualTo("The Email field is not a valid e-mail address."));  // Adjust based on validation message
        }

        [Test]
        public void User_DefaultIsAdmin_ReturnsFalse()
        {
            // Arrange
            var user = new User
            {
                Username = "testuser",
                Email = "testuser@example.com",
                PasswordHash = "hashedpassword",
                Salt = "randomsalt"
            };

            // Act & Assert
            Assert.That(user.IsAdmin, Is.False);  // Default IsAdmin should be false
        }

        [Test]
        public void User_DefaultCreatedAt_ReturnsCurrentUtcDateTime()
        {
            // Arrange
            var user = new User
            {
                Username = "testuser",
                Email = "testuser@example.com",
                PasswordHash = "hashedpassword",
                Salt = "randomsalt"
            };

            // Act
            var createdAtDate = user.CreatedAt;
            var currentUtcDate = DateTime.UtcNow;

            // Assert
            Assert.That(createdAtDate.Date, Is.EqualTo(currentUtcDate.Date));  // CreatedAt should be current UTC date
        }

        [Test]
        public void User_DefaultImageUrl_ReturnsPlaceholderUrl()
        {
            // Arrange
            var user = new User
            {
                Username = "testuser",
                Email = "testuser@example.com",
                PasswordHash = "hashedpassword",
                Salt = "randomsalt"
            };

            // Act
            var imageUrl = user.ImageUrl;

            // Assert
            Assert.That(imageUrl, Is.EqualTo("https://via.placeholder.com/150"));  // Default ImageUrl should be placeholder
        }
    }
}