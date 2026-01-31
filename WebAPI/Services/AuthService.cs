using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Interface;
using WebApi.Models.jwtToken;
using ResetPasswordRequest = Microsoft.AspNetCore.Identity.Data.ResetPasswordRequest;


namespace WebApi.Services
{
    public class AuthService(DataContext context, IConfiguration configuration, IEmailService emailService) : IAuthService
    {

        public async Task<TokenResponseDto?> LoginAsync(UserDto request)
        {
            var user = await context.AuthUser.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user is null)
            {
                return null;
            }
            if (new PasswordHasher<AuthUser>().VerifyHashedPassword(user, user.PasswordHash, request.Password)
                == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return await CreateTokenResponse(user);
        }

        private async Task<TokenResponseDto> CreateTokenResponse(AuthUser? user)
        {
            return new TokenResponseDto
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user)
            };
        }

        public async Task<AuthUser?> RegisterAsync(UserDto request)
        {
            if (await context.AuthUser.AnyAsync(u => u.Username == request.Username))
            {
                return null;
            }

            var user = new AuthUser();
            var hashedPassword = new PasswordHasher<AuthUser>()
                .HashPassword(user, request.Password);

            user.Username = request.Username;
            user.Email = request.Email;
            user.PasswordHash = hashedPassword;

            context.AuthUser.Add(user);
            await context.SaveChangesAsync();

            return user;
        }

        public async Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request)
        {
            var user = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
            if (user is null)
                return null;

            return await CreateTokenResponse(user);
        }

        private async Task<AuthUser?> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
        {
            var user = await context.AuthUser.FindAsync(userId);
            if (user is null || user.RefreshToken != refreshToken
                || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }

            return user;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<string> GenerateAndSaveRefreshTokenAsync(AuthUser user)
        {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await context.SaveChangesAsync();
            return refreshToken;
        }

        private string CreateToken(AuthUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
        public async Task<bool> ForgotPassword(string email)
        {
            var user = await context.AuthUser.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return false;

            var code = new Random().Next(100000, 999999).ToString();
            user.VerificationCode = code;
            user.CodeExpires = DateTime.UtcNow.AddMinutes(15);

            try
            {
                await context.SaveChangesAsync();

                string subject = "Your Password Reset Code";
                string body = $"<h1>Code: {code}</h1>";

                // Try to send the email
                await emailService.SendEmailAsync(email, subject, body);
                return true;
            }
            catch (Exception ex)
            {
                // If email fails, we log it and throw an error so the frontend knows
                // Log ex.Message somewhere if you have logging
                throw new AppException("Failed to send email. Please check your SMTP settings or App Password.");
            }
        }

        public async Task<bool> ResetPassword(ResetPasswordRequest request)
        {
            // 1. Find user by email
            var user = await context.AuthUser.FirstOrDefaultAsync(u => u.Email == request.Email);

            // 2. Validate user existence, the code match, and that it hasn't expired
            if (user == null ||
                user.VerificationCode != request.ResetCode ||
                user.CodeExpires < DateTime.UtcNow)
            {
                // Using your project's AppException helper
                throw new AppException("The verification code is invalid or has expired.");
            }

            // 3. Hash the new password (Assuming BCrypt is used based on typical WebApi setups)
            var passwordHasher = new PasswordHasher<AuthUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, request.NewPassword);

            // 4. Clear the verification fields so the code cannot be used again
            user.VerificationCode = null;
            user.CodeExpires = null;

            await context.SaveChangesAsync();
            return true;
        }


    }
}
