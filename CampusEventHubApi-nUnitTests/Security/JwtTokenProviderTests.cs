using CampusEventHubApi.Security;
using NUnit.Framework;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;

namespace CampusEventHubApi.Tests.Security
{
    [TestFixture]
    public class JwtTokenProviderTests
    {
        private const string SecureKey = "TestSecureKeyForJwt";
        private const int ExpiryMinutes = 30;
        private const int UserId = 123;
        private const string Username = "TestUser";
        private const string Role = "User";

        [Test]
        public void CreateToken_WithEmptySecureKey_ShouldThrowException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => JwtTokenProvider.CreateToken(string.Empty, ExpiryMinutes, UserId, Username, Role));
        }
    }
}
