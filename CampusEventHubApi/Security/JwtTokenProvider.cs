using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CampusEventHubApi.Security
{
    public static class JwtTokenProvider
    {
        public static string CreateToken(string secureKey, int expiryMinutes, int userId, string username, string role)
        {
            var claims = new[]
            {
        new Claim("IDUser", userId.ToString()),
        new Claim(ClaimTypes.Name, username),
        new Claim(ClaimTypes.Role, role)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
