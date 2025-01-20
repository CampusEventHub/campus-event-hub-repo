using CampusEventHubApi.Security;
using NUnit.Framework;
using System;

namespace CampusEventHubApi.Tests.Security
{
    [TestFixture]
    public class PasswordHashProviderTests
    {
        private const string Password = "TestPassword123!";
        private string salt;
        private string hash;

        [SetUp]
        public void SetUp()
        {
            // Generate salt and hash for testing
            salt = PasswordHashProvider.GetSalt();
            hash = PasswordHashProvider.GetHash(Password, salt);
        }

        [Test]
        public void GetSalt_ShouldGenerateValidSalt()
        {
            // Act
            var generatedSalt = PasswordHashProvider.GetSalt();

            // Assert
            Assert.That(generatedSalt, Is.Not.Null.Or.Empty);
            Assert.That(generatedSalt.Length, Is.EqualTo(24));  // Base64 string length (16 bytes)
        }

        [Test]
        public void GetHash_ShouldGenerateCorrectHash()
        {
            // Act
            var generatedHash = PasswordHashProvider.GetHash(Password, salt);

            // Assert
            Assert.That(generatedHash, Is.Not.Null.Or.Empty);
            Assert.That(generatedHash.Length, Is.GreaterThan(0));
        }

        [Test]
        public void VerifyPassword_ShouldReturnTrue_WhenPasswordIsCorrect()
        {
            // Act
            var isValid = PasswordHashProvider.VerifyPassword(Password, hash, salt);

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        public void VerifyPassword_ShouldReturnFalse_WhenPasswordIsIncorrect()
        {
            // Act
            var isValid = PasswordHashProvider.VerifyPassword("WrongPassword", hash, salt);

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        public void VerifyPassword_ShouldReturnFalse_WhenSaltIsDifferent()
        {
            // Act
            var newSalt = PasswordHashProvider.GetSalt();
            var isValid = PasswordHashProvider.VerifyPassword(Password, hash, newSalt);

            // Assert
            Assert.That(isValid, Is.False);
        }
    }
}
