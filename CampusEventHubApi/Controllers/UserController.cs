using CampusEventHubApi.DTOs;
using CampusEventHubApi.Models;
using CampusEventHubApi.Security;
using CampusEventHubApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusEventHubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManagementService _userService;

        public UserController(IUserManagementService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetUsersAsync();
            var userResponses = users.Select(user => new UserResponseDto
            {
                Username = user.Username,
                Email = user.Email
            });

            return Ok(userResponses);
        }


        // GET: api/User/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("User not found.");

            var response = new
            {
                Username = user.Username,
                Email = user.Email
            };

            return Ok(response);
        }


        // PUT: api/User/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data provided.");

            var existingUser = await _userService.GetUserByIdAsync(id);
            if (existingUser == null)
                return NotFound("User not found.");

            if (!string.IsNullOrEmpty(request.Username))
                existingUser.Username = request.Username;

            if (!string.IsNullOrEmpty(request.Email))
                existingUser.Email = request.Email;

            if (!string.IsNullOrEmpty(request.Password))
            {
                var salt = PasswordHashProvider.GetSalt();
                var hashedPassword = PasswordHashProvider.GetHash(request.Password, salt);
                existingUser.PasswordHash = hashedPassword;
                existingUser.Salt = salt;
            }

            var result = await _userService.UpdateUserAsync(id, existingUser);
            if (!result)
                return BadRequest("Failed to update user.");

            return Ok(new { Message = "User updated successfully." });
        }


        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result)
                return NotFound("User not found.");

            return Ok("User deleted successfully.");
        }
    }
}
