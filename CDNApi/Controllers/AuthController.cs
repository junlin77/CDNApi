using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using CDNApi.Data;
using CDNApi.Models;
using CDNApi.Utils;
using CDNApi.Configuration;

namespace CDNApi.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly TokenUtils _tokenUtils;
        private readonly JwtSettings _jwtSettings;

        public AuthController(AppDbContext context, IOptions<JwtSettings> jwtSettings, TokenUtils tokenUtils)
        {
            _context = context;
            _jwtSettings = jwtSettings.Value;
            _tokenUtils = tokenUtils;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(ApiUser user)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);  
            _context.ApiUsers.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] ApiUser loginUser)
        {
            var user = await _context.ApiUsers
                .FirstOrDefaultAsync(u => u.Username == loginUser.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginUser.PasswordHash, user.PasswordHash))
                return Unauthorized(new { message = "Invalid credentials" });

            // Generate access and refresh tokens
            var accessToken = _tokenUtils.GenerateAccessToken(user, _jwtSettings.SecretKey);
            var refreshToken = _tokenUtils.GenerateRefreshToken();

            // Store the refresh token and expiry date
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryDate = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays); 
            await _context.SaveChangesAsync();

            return Ok(new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenResponse tokenResponse)
        {
            var user = await _context.ApiUsers
            .FirstOrDefaultAsync(u => u.RefreshToken == tokenResponse.RefreshToken);

            if (user == null || user.RefreshTokenExpiryDate < DateTime.UtcNow)
                return Unauthorized(new { message = "Invalid or expired refresh token." });
            
            // Generate a new access token and a new refresh token
            var newAccessToken = _tokenUtils.GenerateAccessToken(user, _jwtSettings.SecretKey);
            var newRefreshToken = _tokenUtils.GenerateRefreshToken();

            // Update the user's refresh token and expiration date
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryDate = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays); 
            await _context.SaveChangesAsync();

            return Ok(new TokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }
    }
}

