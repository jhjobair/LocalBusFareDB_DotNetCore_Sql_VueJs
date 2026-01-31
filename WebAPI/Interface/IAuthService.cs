using WebApi.Entities;
using WebApi.Models.jwtToken;

namespace WebApi.Interface
{
    public interface IAuthService
    {

        Task<AuthUser?> RegisterAsync(UserDto request);
        Task<TokenResponseDto?> LoginAsync(UserDto request);
        Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request);
        Task<bool> ForgotPassword(string email);
        Task<bool> ResetPassword(Microsoft.AspNetCore.Identity.Data.ResetPasswordRequest request);
    }
}

