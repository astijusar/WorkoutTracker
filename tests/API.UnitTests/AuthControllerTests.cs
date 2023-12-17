using System.Security.Claims;
using API.Controllers;
using AutoMapper;
using Core.Models;
using Core.Models.DTOs.JWT;
using Core.Models.DTOs.User;
using Core.Options;
using Core.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Moq;

namespace API.UnitTests
{
    public class AuthControllerTests
    {
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly Mock<JwtTokenService> _mockJwtTokenService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AuthController _authController;

        public AuthControllerTests()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            var mockIOptions = new Mock<IOptions<JwtOptions>>();
            mockIOptions.Setup(x => x.Value).Returns(new JwtOptions()
                { Secret = "Yzvgeodxi9P2sOTRXYEY1PWL1n6Bt2W3", ValidAudience = "Audience", ValidIssuer = "Issuer" });

            _mockUserManager = new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            _mockUserManager.Object.UserValidators.Add(new UserValidator<User>());
            _mockUserManager.Object.PasswordValidators.Add(new PasswordValidator<User>());
            _mockJwtTokenService = new Mock<JwtTokenService>(mockIOptions.Object);
            _mockMapper = new Mock<IMapper>();
            _authController = new AuthController(_mockUserManager.Object, _mockJwtTokenService.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsOkResult()
        {
            // Arrange
            var loginDto = new UserLoginDto("TestUser", "TestPassword");
            var user = new User { UserName = "TestUser", PasswordHash = "TestPassword" };
            var roles = new List<string> { "User" };

            _mockUserManager.Setup(x => x.GetRolesAsync(user)).Returns(Task.FromResult<IList<string>>(roles));
            _mockUserManager.Setup(x => x.FindByNameAsync(loginDto.UserName)).ReturnsAsync(user);
            _mockUserManager.Setup(x => x.CheckPasswordAsync(user, loginDto.Password)).ReturnsAsync(true);

            // Act
            var result = await _authController.Login(loginDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Register_ValidUser_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var registrationDto = new UserRegistrationDto("TestUser", "email@email.com", "TestPassword");
            var user = new User { UserName = "TestUser", Email = "email@email.com", PasswordHash = "TestPassword" };
            _mockUserManager.Setup(x => x.FindByNameAsync(registrationDto.UserName)).ReturnsAsync((User)null);
            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), registrationDto.Password)).ReturnsAsync(IdentityResult.Success);
            _mockMapper.Setup(x => x.Map<User>(registrationDto)).Returns(user);

            // Act
            var result = await _authController.Register(registrationDto);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Fact]
        public async Task GetAccessToken_ValidRefreshToken_ReturnsOkResult()
        {
            // Arrange
            var refreshAccessTokenDto = new RefreshAccessTokenDto("TestPassword");
            var user = new User { UserName = "TestUser", PasswordHash = "TestPassword", Id = "1"};
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, user.Id)
            };
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(authClaims, "jwt"));
            var roles = new List<string> { "User" };

            _mockUserManager.Setup(x => x.GetRolesAsync(user)).Returns(Task.FromResult<IList<string>>(roles));
            _mockJwtTokenService.Setup(x => x.TryParseRefreshToken(refreshAccessTokenDto.RefreshToken, out claimsPrincipal)).Returns(true);
            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

            // Act
            var result = await _authController.GetAccessToken(refreshAccessTokenDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
