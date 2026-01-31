using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Interface;
using WebApi.Models.jwtToken;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        //[AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            var user = await authService.RegisterAsync(request);
            if (user is null)
                return BadRequest("Username already exists.");

            return Ok(user);
        }


        //[AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UserDto request)
        {
            var result = await authService.LoginAsync(request);
            if (result is null)
                return BadRequest("Invalid username or password.");

            return Ok(result);
        }



        //[AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            var result = await authService.RefreshTokensAsync(request);
            if (result is null || result.AccessToken is null || result.RefreshToken is null)
                return Unauthorized("Invalid refresh token.");

            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AuthenticatedOnlyEndpoint()
        {
            return Ok("You are authenticated!");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok("You are and admin!");
        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest model)
        {
            var result = await authService.ForgotPassword(model.Email);

            // For security, we usually return 200 even if the email isn't found
            // but here we return a specific message for your testing
            if (!result)
                return BadRequest(new { message = "Email not found." });

            return Ok(new { message = "A verification code has been sent to your email." });
        }

        // 2. Reset Password Endpoint
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] Microsoft.AspNetCore.Identity.Data.ResetPasswordRequest request)
        {
            try
            {
                await authService.ResetPassword(request);
                return Ok(new { message = "Your password has been reset successfully." });
            }
            catch (AppException ex)
            {
                // Catches the 'Invalid or expired code' error from AuthService
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while resetting your password." });
            }
        }
    }

    // Small helper DTO for the Forgot Password step
    public class ForgotPasswordRequest
    {
        public string Email { get; set; } = string.Empty;
    }
}

