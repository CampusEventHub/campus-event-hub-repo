using CampusEventHubApi.DTOs;
using CampusEventHubApi.Factories;
using CampusEventHubApi.Models;
using NUnit.Framework;
using System;

namespace CampusEventHubApi.Tests.Factories
{
    [TestFixture]
    public class OcjenaDtoFactoryTests
    {
        [Test]
        public void CreateResponseDto_ShouldMapOcjenaToOcjenaResponseDto()
        {
            // Arrange
            var ocjena = new Ocjena
            {
                OcjenaID = 1,
                EventID = 100,
                Event = new Event { Title = "Test Event" },
                UserID = 200,
                User = new User { Username = "TestUser" },
                OcjenaVrijednost = 5,
                Komentar = "Great event!",
                Kreirano = new DateTime(2025, 1, 20)
            };

            // Act
            var dto = OcjenaDtoFactory.CreateResponseDto(ocjena);

            // Assert
            Assert.That(dto.OcjenaID, Is.EqualTo(1));
            Assert.That(dto.EventID, Is.EqualTo(100));
            Assert.That(dto.EventNaslov, Is.EqualTo("Test Event")); // Check Event title
            Assert.That(dto.UserID, Is.EqualTo(200));
            Assert.That(dto.Username, Is.EqualTo("TestUser")); // Check User name
            Assert.That(dto.OcjenaVrijednost, Is.EqualTo(5));
            Assert.That(dto.Komentar, Is.EqualTo("Great event!"));
            Assert.That(dto.Kreirano, Is.EqualTo(new DateTime(2025, 1, 20))); // Check creation date
        }

        [Test]
        public void CreateResponseDto_ShouldHandleNullEventAndUser()
        {
            // Arrange
            var ocjena = new Ocjena
            {
                OcjenaID = 1,
                EventID = 100,
                Event = null, // Event is null
                UserID = 200,
                User = null, // User is null
                OcjenaVrijednost = 5,
                Komentar = "Great event!",
                Kreirano = new DateTime(2025, 1, 20)
            };

            // Act
            var dto = OcjenaDtoFactory.CreateResponseDto(ocjena);

            // Assert
            Assert.That(dto.EventNaslov, Is.Null); // Event title should be null
            Assert.That(dto.Username, Is.Null); // User name should be null
        }

        [Test]
        public void CreateRequestDto_ShouldMapOcjenaToOcjenaRequestDto()
        {
            // Arrange
            var ocjena = new Ocjena
            {
                EventID = 100,
                UserID = 200,
                OcjenaVrijednost = 5,
                Komentar = "Great event!"
            };

            // Act
            var dto = OcjenaDtoFactory.CreateRequestDto(ocjena);

            // Assert
            Assert.That(dto.EventID, Is.EqualTo(100));
            Assert.That(dto.UserID, Is.EqualTo(200));
            Assert.That(dto.OcjenaVrijednost, Is.EqualTo(5));
            Assert.That(dto.Komentar, Is.EqualTo("Great event!"));
        }

        [Test]
        public void CreateUpdateDto_ShouldMapOcjenaToOcjenaUpdateDto()
        {
            // Arrange
            var ocjena = new Ocjena
            {
                OcjenaVrijednost = 4,
                Komentar = "Good event"
            };

            // Act
            var dto = OcjenaDtoFactory.CreateUpdateDto(ocjena);

            // Assert
            Assert.That(dto.OcjenaVrijednost, Is.EqualTo(4));
            Assert.That(dto.Komentar, Is.EqualTo("Good event"));
        }
    }
}