using CampusEventHubApi.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CampusEventHubApi.Tests.Models
{
    [TestFixture]
    public class OcjenaTests
    {
        [Test]
        public void Ocjena_ValidModel_ReturnsValid()
        {
            // Arrange
            var ocjena = new Ocjena
            {
                EventID = 1,
                UserID = 1,
                OcjenaVrijednost = 5,
                Komentar = "Great event!"
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(ocjena, new ValidationContext(ocjena), validationResults, true);

            // Assert
            Assert.That(isValid, Is.True);  // Model should be valid
            Assert.That(validationResults.Count, Is.EqualTo(0));  // No validation errors
        }

        [Test]
        public void Ocjena_InvalidOcjenaVrijednost_ReturnsValidationError()
        {
            // Arrange
            var ocjena = new Ocjena
            {
                EventID = 1,
                UserID = 1,
                OcjenaVrijednost = 6,  // Invalid value (must be between 1 and 5)
                Komentar = "Great event!"
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(ocjena, new ValidationContext(ocjena), validationResults, true);

            // Assert
            Assert.That(isValid, Is.False);  // Model should be invalid
            Assert.That(validationResults.Count, Is.EqualTo(1));  // There should be one validation error
            Assert.That(validationResults[0].ErrorMessage, Is.EqualTo("The field OcjenaVrijednost must be between 1 and 5.")); // Adjust to match your validation message
        }

        [Test]
        public void Ocjena_DefaultKreirano_ReturnsCurrentDateTime()
        {
            // Arrange
            var ocjena = new Ocjena
            {
                EventID = 1,
                UserID = 1,
                OcjenaVrijednost = 5,
                Komentar = "Excellent event!"
            };

            // Act
            var currentDate = DateTime.Now.Date;
            var createdDate = ocjena.Kreirano.Date;

            // Assert
            Assert.That(createdDate, Is.EqualTo(currentDate));  // The date should be today's date
        }

        [Test]
        public void Ocjena_ValidKomentar_ReturnsValid()
        {
            // Arrange
            var ocjena = new Ocjena
            {
                EventID = 1,
                UserID = 1,
                OcjenaVrijednost = 4,
                Komentar = "Nice event!"
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(ocjena, new ValidationContext(ocjena), validationResults, true);

            // Assert
            Assert.That(isValid, Is.True);  // Model should be valid
            Assert.That(validationResults.Count, Is.EqualTo(0));  // No validation errors
        }
    }
}