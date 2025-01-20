using CampusEventHubApi.Models;
using CampusEventHubApi.Services.Observers;
using NUnit.Framework;
using System;
using System.IO;

namespace CampusEventHubApi.Tests.Services.Observers
{
    [TestFixture]
    public class EmailNotifierTests
    {
        private StringWriter _consoleOutput;
        private EmailNotifier _emailNotifier;

        [SetUp]
        public void Setup()
        {
            _consoleOutput = new StringWriter();
            Console.SetOut(_consoleOutput);
            _emailNotifier = new EmailNotifier();
        }

        [TearDown]
        public void TearDown()
        {
            _consoleOutput.Dispose();
            Console.SetOut(Console.Out); // Reset to the original output
        }

        [Test]
        public void OnEventCreated_ShouldWriteCorrectMessage()
        {
            // Arrange
            var kalendar = new Kalendar
            {
                Naslov = "Test Event"
            };

            // Act
            _emailNotifier.OnEventCreated(kalendar);

            // Assert
            var expectedMessage = "[Email] Novi događaj 'Test Event' kreiran.\r\n";
            Assert.That(_consoleOutput.ToString(), Is.EqualTo(expectedMessage));
        }

        [Test]
        public void OnEventUpdated_ShouldWriteCorrectMessage()
        {
            // Arrange
            var kalendar = new Kalendar
            {
                Naslov = "Updated Event"
            };

            // Act
            _emailNotifier.OnEventUpdated(kalendar);

            // Assert
            var expectedMessage = "[Email] Događaj 'Updated Event' ažuriran.\r\n";
            Assert.That(_consoleOutput.ToString(), Is.EqualTo(expectedMessage));
        }

        [Test]
        public void OnEventDeleted_ShouldWriteCorrectMessage()
        {
            // Arrange
            int kalendarId = 123;

            // Act
            _emailNotifier.OnEventDeleted(kalendarId);

            // Assert
            var expectedMessage = "[Email] Događaj s ID '123' je izbrisan.\r\n";
            Assert.That(_consoleOutput.ToString(), Is.EqualTo(expectedMessage));
        }
    }
}