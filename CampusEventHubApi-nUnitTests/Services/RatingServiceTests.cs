using CampusEventHubApi.Services;
using CampusEventHubApi.Services.Strategies;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CampusEventHubApi.Tests.Services
{
    [TestFixture]
    public class RatingServiceTests
    {
        private RatingService _ratingService;
        private Mock<IRatingCalculationStrategy> _mockStrategy;

        [SetUp]
        public void SetUp()
        {
            _ratingService = new RatingService();
            _mockStrategy = new Mock<IRatingCalculationStrategy>();
        }

        [Test]
        public void SetStrategy_ShouldThrowArgumentNullException_WhenNullStrategyIsPassed()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _ratingService.SetStrategy(null));
        }

        [Test]
        public void GetAverageRating_ShouldThrowInvalidOperationException_WhenNoStrategyIsSet()
        {
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _ratingService.GetAverageRating(new List<int> { 4, 5 }));
        }

        [Test]
        public void GetAverageRating_ShouldThrowInvalidOperationException_WhenNoValidRatings()
        {
            // Arrange
            _mockStrategy.Setup(s => s.CalculateAverageRating(It.IsAny<IEnumerable<int>>())).Returns(0);
            _ratingService.SetStrategy(_mockStrategy.Object);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _ratingService.GetAverageRating(new List<int> { 6, 7, 8 }));
        }

        [Test]
        public void GetAverageRating_ShouldReturnCorrectAverage_WhenValidRatingsAreProvided()
        {
            // Arrange
            _mockStrategy.Setup(s => s.CalculateAverageRating(It.IsAny<IEnumerable<int>>())).Returns(4.5);
            _ratingService.SetStrategy(_mockStrategy.Object);

            // Act
            var result = _ratingService.GetAverageRating(new List<int> { 4, 5 });

            // Assert
            Assert.That(result, Is.EqualTo(4.5));
        }

        [Test]
        public void SetStrategy_ShouldSetTheStrategyCorrectly()
        {
            // Arrange
            var strategy = new Mock<IRatingCalculationStrategy>().Object;

            // Act
            _ratingService.SetStrategy(strategy);

            // Assert
            Assert.DoesNotThrow(() => _ratingService.GetAverageRating(new List<int> { 4, 5 }));
        }
    }
}
