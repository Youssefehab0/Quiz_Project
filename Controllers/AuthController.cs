using Microsoft.AspNetCore.Mvc;
using Quiz_project.Dtos;
using Quiz_project.Models;
using Quiz_project.Repositories;

namespace Quiz_project.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
                return BadRequest("User already exists.");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                Role = dto.Role
            };

            await _userRepository.AddAsync(user);
            var saved = await _userRepository.SaveChangesAsync();

            if (!saved)
                return StatusCode(500, "Something went wrong while saving user.");

            return Ok(new { message = "User registered successfully" });
        }

        // ✅ Login Endpoint
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAndPasswordAsync(dto.Email, dto.Password);
            if (user == null)
                return Unauthorized("Invalid email or password");

            return Ok(new
            {
                message = "Login successful",
                userId = user.Id,
                name = user.Name,
                email = user.Email,
                role = user.Role
            });
        }
    }
}
