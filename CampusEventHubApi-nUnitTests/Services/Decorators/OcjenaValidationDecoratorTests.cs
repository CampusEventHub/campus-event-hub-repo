using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using CampusEventHubApi.DTOs;
using CampusEventHubApi.Services.Decorators;
using CampusEventHubApi.Services.Interfaces;

namespace CampusEventHubApi.Tests
{
    [TestFixture]
    public class OcjenaValidationDecoratorTests
    {
        private Mock<IOcjenaService> _mockInnerService;
        private OcjenaValidationDecorator _decorator;

        [SetUp]
        public void Setup()
        {
            _mockInnerService = new Mock<IOcjenaService>();
            _decorator = new OcjenaValidationDecorator(_mockInnerService.Object);
        }

        [Test]
        public async Task CreateAsync_ShouldThrowArgumentException_WhenOcjenaVrijednostIsOutOfRange()
        {
            var request = new OcjenaRequestDto
            {
                OcjenaVrijednost = 6 // Invalid value
            };

            var ex = Assert.ThrowsAsync<ArgumentException>(() => _decorator.CreateAsync(request));
            Assert.That(ex.Message, Is.EqualTo("Ocjena mora biti između 1 i 5."));
        }

        [Test]
        public async Task CreateAsync_ShouldThrowArgumentException_WhenOcjenaVrijednostIsLowAndNoComment()
        {
            var request = new OcjenaRequestDto
            {
                OcjenaVrijednost = 2,  // Valid value but comment is missing
                Komentar = string.Empty
            };

            var ex = Assert.ThrowsAsync<ArgumentException>(() => _decorator.CreateAsync(request));
            Assert.That(ex.Message, Is.EqualTo("Komentar je obavezan za ocjene manje ili jednake 2."));
        }

        [Test]
        public async Task CreateAsync_ShouldForwardToInnerService_WhenRequestIsValid()
        {
            var request = new OcjenaRequestDto
            {
                OcjenaVrijednost = 4, // Valid value
                Komentar = "Good event!"
            };

            _mockInnerService.Setup(service => service.CreateAsync(request))
                .ReturnsAsync(new OcjenaResponseDto()); // Mock response from inner service

            var result = await _decorator.CreateAsync(request);

            _mockInnerService.Verify(service => service.CreateAsync(request), Times.Once);
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task UpdateAsync_ShouldThrowArgumentException_WhenOcjenaVrijednostIsOutOfRange()
        {
            var request = new OcjenaUpdateDto
            {
                OcjenaVrijednost = 6 // Invalid value
            };

            var ex = Assert.ThrowsAsync<ArgumentException>(() => _decorator.UpdateAsync(1, request));
            Assert.That(ex.Message, Is.EqualTo("Ocjena mora biti između 1 i 5."));
        }

        [Test]
        public async Task UpdateAsync_ShouldThrowArgumentException_WhenOcjenaVrijednostIsLowAndNoComment()
        {
            var request = new OcjenaUpdateDto
            {
                OcjenaVrijednost = 2,  // Valid value but comment is missing
                Komentar = string.Empty
            };

            var ex = Assert.ThrowsAsync<ArgumentException>(() => _decorator.UpdateAsync(1, request));
            Assert.That(ex.Message, Is.EqualTo("Komentar je obavezan za ocjene manje ili jednake 2."));
        }

        [Test]
        public async Task UpdateAsync_ShouldForwardToInnerService_WhenRequestIsValid()
        {
            var request = new OcjenaUpdateDto
            {
                OcjenaVrijednost = 4, // Valid value
                Komentar = "Good event!"
            };

            _mockInnerService.Setup(service => service.UpdateAsync(1, request))
                .ReturnsAsync(true); // Mock response from inner service

            var result = await _decorator.UpdateAsync(1, request);

            _mockInnerService.Verify(service => service.UpdateAsync(1, request), Times.Once);
            Assert.That(result, Is.True);
        }
    }
}