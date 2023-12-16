using System.Security.Claims;
using Core.Options;
using Core.Services;
using Microsoft.Extensions.Options;
using FluentAssertions;

namespace test.Core.Services
{
    public class JwtTokenServiceTests
    {
        private readonly JwtTokenService _jwtTokenService;
        private readonly JwtOptions _jwtOptions;

        public JwtTokenServiceTests()
        {
            _jwtOptions = new JwtOptions
            {
                Secret = "ThisIsASecretKeyForTesting",
                ValidIssuer = "TestIssuer",
                ValidAudience = "TestAudience"
            };

            _jwtTokenService = new JwtTokenService(Options.Create(_jwtOptions));
        }

        [Fact]
        public void CreateAccessToken_ShouldReturnValidToken()
        {
            var userId = Guid.NewGuid().ToString();
            var userName = "TestUser";
            var roles = new List<string> { "Admin", "User" };

            var token = _jwtTokenService.CreateAccessToken(userId, userName, roles);

            token.Should().NotBeNull();
            token.Should().BeOfType<string>();
        }

        [Fact]
        public void CreateRefreshToken_ShouldReturnValidToken()
        {
            var userId = Guid.NewGuid().ToString();

            var token = _jwtTokenService.CreateRefreshToken(userId);

            token.Should().NotBeNull();
            token.Should().BeOfType<string>();
        }

        [Fact]
        public void TryParseRefreshToken_ShouldReturnTrueForValidToken()
        {
            var userId = Guid.NewGuid().ToString();

            var token = _jwtTokenService.CreateRefreshToken(userId);

            var result = _jwtTokenService.TryParseRefreshToken(token, out ClaimsPrincipal? claims);

            result.Should().BeTrue();
            claims.Should().NotBeNull();
        }

        [Fact]
        public void TryParseRefreshToken_ShouldReturnFalseForInvalidToken()
        {
            var token = "InvalidToken";

            var result = _jwtTokenService.TryParseRefreshToken(token, out ClaimsPrincipal? claims);

            result.Should().BeFalse();
            claims.Should().BeNull();
        }
    }
}
