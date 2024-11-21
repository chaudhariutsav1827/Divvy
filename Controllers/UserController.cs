using Divvy.DTOs;
using Divvy.Models;
using Divvy.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Divvy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var (token, message) = await userService.LoginUserAsync(loginRequest);

            if (token == null)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = message,
                    Data = null
                });
            }

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = message,
                Data = token
            });
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest signUpRequest)
        {
            var (user, message) = await userService.SignUpUserAsync(signUpRequest);
            if (user == null)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = message,
                    Data = null
                });
            }
            return Ok(new ApiResponse<User>
            {
                Success = true,
                Data = user,
                Message = message
            });
        }
    }
}