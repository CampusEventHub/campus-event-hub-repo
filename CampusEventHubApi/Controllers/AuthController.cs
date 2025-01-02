using CampusEventHubApi.DTOs;
using CampusEventHubApi.Models;
using CampusEventHubApi.Services.Interfaces;
using CampusEventHubApi.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace CampusEventHubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly IUserManagementService _userService;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthenticationService authService, IUserManagementService userService, IConfiguration configuration)
        {
            _authService = authService;
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterUserAsync(request.Username, request.Email, request.Password);
            if (!result)
                return BadRequest("Username or email already exists.");

            return Ok("User successfully registered.");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _authService.AuthenticateUserAsync(request.Username, request.Password);
            if (user == null)
                return Unauthorized("Invalid username or password.");

            var token = JwtTokenProvider.CreateToken(
                _configuration["JWT:SecureKey"],
                120,
                user.IDUser,
                user.Username,
                user.IsAdmin ? "Admin" : "User");

            return Ok(new { Token = token, user.IDUser, user.Username, user.IsAdmin });
        }

        [HttpPut("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data provided.");

            var userIdClaim = User.FindFirst("IDUser");
            if (userIdClaim == null)
                return Unauthorized("User not authenticated.");

            int userId = int.Parse(userIdClaim.Value);

            var result = await _userService.ChangePasswordAsync(userId, request.OldPassword, request.NewPassword);
            if (!result)
                return BadRequest("Old password is incorrect or user not found.");

            return Ok(new { Message = "Password successfully updated." });
        }
    }
}
