using CampusEventHubApi.DTOs;
using CampusEventHubApi.Factories;
using CampusEventHubApi.Models;
using NUnit.Framework;
using System;

namespace CampusEventHubApi.Tests.Factories
{
    [TestFixture]
    public class KalendarDtoFactoryTests
    {
        [Test]
        public void CreateRequestDto_ShouldMapKalendarToKalendarRequestDto()
        {
            // Arrange
            var kalendar = new Kalendar
            {
                Naslov = "Test Event",
                Opis = "Test Description",
                Datum = new DateTime(2025, 1, 20),
                Vrijeme = new TimeSpan(14, 30, 0)
            };

            // Act
            var dto = KalendarDtoFactory.CreateRequestDto(kalendar);

            // Assert
            Assert.That(dto.Naslov, Is.EqualTo("Test Event"));
            Assert.That(dto.Opis, Is.EqualTo("Test Description"));
            Assert.That(dto.Datum, Is.EqualTo("20.01.2025")); // Date format check
            Assert.That(dto.Vrijeme, Is.EqualTo("14:30")); // Time format check
        }

        [Test]
        public void CreateResponseDto_ShouldMapKalendarToKalendarResponseDto()
        {
            // Arrange
            var kalendar = new Kalendar
            {
                KalendarID = 1,
                Naslov = "Test Event",
                Opis = "Test Description",
                Datum = new DateTime(2025, 1, 20),
                Vrijeme = new TimeSpan(14, 30, 0),
                Kreirano = new DateTime(2025, 1, 15, 10, 0, 0)
            };

            // Act
            var dto = KalendarDtoFactory.CreateResponseDto(kalendar);

            // Assert
            Assert.That(dto.KalendarID, Is.EqualTo(1));
            Assert.That(dto.Naslov, Is.EqualTo("Test Event"));
            Assert.That(dto.Opis, Is.EqualTo("Test Description"));
            Assert.That(dto.Datum, Is.EqualTo("20.01.2025")); // Date format check
            Assert.That(dto.Vrijeme, Is.EqualTo("14:30")); // Time format check
            Assert.That(dto.Kreirano, Is.EqualTo("15.01.2025 10:00:00")); // Creation date-time format check
        }

        [Test]
        public void CreateRequestDto_ShouldHandleNullVrijeme()
        {
            // Arrange
            var kalendar = new Kalendar
            {
                Naslov = "Test Event",
                Opis = "Test Description",
                Datum = new DateTime(2025, 1, 20),
                Vrijeme = null // No time specified
            };

            // Act
            var dto = KalendarDtoFactory.CreateRequestDto(kalendar);

            // Assert
            Assert.That(dto.Vrijeme, Is.Null); // Ensure time is null in the DTO
        }
    }
}