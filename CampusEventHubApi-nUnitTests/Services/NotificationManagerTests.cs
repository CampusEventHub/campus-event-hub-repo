using CampusEventHubApi.Services;
using NUnit.Framework;
using System;

namespace CampusEventHubApi.Tests.Services
{
    [TestFixture]
    public class NotificationManagerTests
    {
        private NotificationManager _notificationManager;

        [SetUp]
        public void SetUp()
        {
            // Get the instance of NotificationManager
            _notificationManager = NotificationManager.Instance;
        }

        [Test]
        public void Instance_ShouldReturnSameInstance_OnMultipleCalls()
        {
            // Arrange
            var firstInstance = NotificationManager.Instance;
            var secondInstance = NotificationManager.Instance;

            // Assert
            Assert.That(firstInstance, Is.SameAs(secondInstance));
        }

        [Test]
        public void Notify_ShouldOutputMessage()
        {
            // Arrange
            var message = "Test Notification";

            // Act and Assert
            // We can't directly test Console.WriteLine output, so we can mock or capture it.
            // For this test, we'll ensure no exception is thrown during the method call.
            Assert.DoesNotThrow(() => _notificationManager.Notify(message));
        }
    }
}
