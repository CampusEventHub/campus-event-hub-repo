using CampusEventHubApi.DTOs;
using CampusEventHubApi.Services.Decorators;
using CampusEventHubApi.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CampusEventHubApi.Tests
{
    [TestFixture]
    public class ValidationDecoratorTests
    {
        private Mock<IKalendarService> _innerServiceMock;
        private ValidationDecorator _decorator;

        [SetUp]
        public void SetUp()
        {
            _innerServiceMock = new Mock<IKalendarService>();
            _decorator = new ValidationDecorator(_innerServiceMock.Object);
        }

        [Test]
        public void CreateAsync_ShouldThrowArgumentException_WhenNaslovIsEmpty()
        {
            // Arrange
            var request = new KalendarRequestDto
            {
                Naslov = "",
                Datum = "01.01.2025"
            };

            // Act & Assert
            Assert.That(async () => await _decorator.CreateAsync(request),
                Throws.ArgumentException.With.Message.EqualTo("Naslov je obavezan."));
        }

        [Test]
        public void CreateAsync_ShouldThrowFormatException_WhenDatumIsInvalid()
        {
            // Arrange
            var request = new KalendarRequestDto
            {
                Naslov = "Event Title",
                Datum = "2025-01-01"
            };

            // Act & Assert
            Assert.That(async () => await _decorator.CreateAsync(request),
            Throws.InstanceOf<FormatException>().With.Message.EqualTo("Neispravan format datuma. Koristite format dd.MM.yyyy."));
        }

        [Test]
        public async Task CreateAsync_ShouldCallInnerService_WhenValidRequest()
        {
            // Arrange
            var request = new KalendarRequestDto
            {
                Naslov = "Valid Event",
                Datum = "01.01.2025"
            };
            _innerServiceMock.Setup(service => service.CreateAsync(It.IsAny<KalendarRequestDto>())).ReturnsAsync(request);

            // Act
            var result = await _decorator.CreateAsync(request);

            // Assert
            Assert.That(result, Is.EqualTo(request));
            _innerServiceMock.Verify(service => service.CreateAsync(It.IsAny<KalendarRequestDto>()), Times.Once);
        }

        [Test]
        public void UpdateAsync_ShouldThrowArgumentException_WhenNaslovIsEmpty()
        {
            // Arrange
            var request = new KalendarRequestDto
            {
                Naslov = "",
                Datum = "01.01.2025"
            };

            // Act & Assert
            Assert.That(async () => await _decorator.UpdateAsync(1, request),
                Throws.ArgumentException.With.Message.EqualTo("Naslov je obavezan."));
        }

        [Test]
        public void UpdateAsync_ShouldThrowFormatException_WhenDatumIsInvalid()
        {
            // Arrange
            var request = new KalendarRequestDto
            {
                Naslov = "Updated Event",
                Datum = "2025-01-01"
            };

            // Act & Assert
            Assert.That(async () => await _decorator.UpdateAsync(1, request),
            Throws.InstanceOf<FormatException>().With.Message.EqualTo("Neispravan format datuma. Koristite format dd.MM.yyyy."));
        }

        [Test]
        public async Task UpdateAsync_ShouldCallInnerService_WhenValidRequest()
        {
            // Arrange
            var request = new KalendarRequestDto
            {
                Naslov = "Updated Event",
                Datum = "01.01.2025"
            };
            _innerServiceMock.Setup(service => service.UpdateAsync(It.IsAny<int>(), It.IsAny<KalendarRequestDto>())).ReturnsAsync(true);

            // Act
            var result = await _decorator.UpdateAsync(1, request);

            // Assert
            Assert.That(result, Is.True);
            _innerServiceMock.Verify(service => service.UpdateAsync(It.IsAny<int>(), It.IsAny<KalendarRequestDto>()), Times.Once);
        }

        [Test]
        public async Task GetByIdAsync_ShouldCallInnerService()
        {
            // Arrange
            var request = new KalendarRequestDto
            {
                Naslov = "Event Title",
                Datum = "01.01.2025"
            };
            _innerServiceMock.Setup(service => service.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(request);

            // Act
            var result = await _decorator.GetByIdAsync(1);

            // Assert
            Assert.That(result, Is.EqualTo(request));
            _innerServiceMock.Verify(service => service.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public async Task GetAllAsync_ShouldCallInnerService()
        {
            // Arrange
            var request = new KalendarRequestDto
            {
                Naslov = "Event Title",
                Datum = "01.01.2025"
            };
            _innerServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(new[] { request });

            // Act
            var result = await _decorator.GetAllAsync();

            // Assert
            Assert.That(result, Is.EqualTo(new[] { request }));
            _innerServiceMock.Verify(service => service.GetAllAsync(), Times.Once);
        }

        [Test]
        public async Task GetByMonthAsync_ShouldCallInnerService()
        {
            // Arrange
            var request = new KalendarRequestDto
            {
                Naslov = "Event Title",
                Datum = "01.01.2025"
            };
            _innerServiceMock.Setup(service => service.GetByMonthAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new[] { request });

            // Act
            var result = await _decorator.GetByMonthAsync(1, 2025);

            // Assert
            Assert.That(result, Is.EqualTo(new[] { request }));
            _innerServiceMock.Verify(service => service.GetByMonthAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_ShouldCallInnerService()
        {
            // Arrange
            _innerServiceMock.Setup(service => service.DeleteAsync(It.IsAny<int>())).ReturnsAsync(true);

            // Act
            var result = await _decorator.DeleteAsync(1);

            // Assert
            Assert.That(result, Is.True);
            _innerServiceMock.Verify(service => service.DeleteAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
