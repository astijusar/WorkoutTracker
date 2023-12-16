using System.Security.Claims;
using API.Filters;
using AutoMapper;
using Core.Models;
using Core.Models.DTOs.JWT;
using Core.Models.DTOs.User;
using Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;

        public AuthController(UserManager<User> userManager, JwtTokenService jwtTokenService, IMapper mapper)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
        }

        /// <summary>
        /// User login
        /// </summary>
        /// <param name="loginDto">User login object</param>
        /// <returns>Access and refresh tokens</returns>
        /// <response code="200">Returns access and refresh tokens</response>
        /// <response code="401">Unauthorized, username or password is incorrect</response>
        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);

            if (user == null)
            {
                return Unauthorized("Username or password is incorrect.");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!isPasswordValid)
            {
                return Unauthorized("Username or password is incorrect.");
            }

            user.ForceRelogin = false;
            await _userManager.UpdateAsync(user);

            var roles = await _userManager.GetRolesAsync(user);

            var accessToken = _jwtTokenService.CreateAccessToken(user.Id, user.UserName, roles);
            var refreshToken = _jwtTokenService.CreateRefreshToken(user.Id);

            return Ok(new SuccessfulLoginDto(accessToken, refreshToken));
        }

        /// <summary>
        /// User registration
        /// </summary>
        /// <param name="registrationDto">User registration object</param>
        /// <returns>User object</returns>
        /// <response code="201">Returns the newly created user</response>
        /// <response code="422">User name already taken</response>
        [HttpPost("register")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto registrationDto)
        {
            var user = await _userManager.FindByNameAsync(registrationDto.UserName);

            if (user != null)
            {
                return UnprocessableEntity("User name already taken");
            }

            var newUser = _mapper.Map<User>(registrationDto);

            var createUserResult = await _userManager.CreateAsync(newUser, registrationDto.Password);

            if (!createUserResult.Succeeded)
            {
                return UnprocessableEntity();
            }

            await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            var userDto = _mapper.Map<UserDto>(newUser);

            return CreatedAtAction(nameof(Login), userDto);
        }

        /// <summary>
        /// Get access token
        /// </summary>
        /// <param name="refreshAccessTokenDto">Refresh access token object</param>
        /// <returns>Access and refresh tokens</returns>
        /// <response code="200">Returns access and refresh tokens</response>
        /// <response code="422">Invalid token</response>
        [HttpPost("refresh")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetAccessToken([FromBody] RefreshAccessTokenDto refreshAccessTokenDto)
        {
            if (!_jwtTokenService.TryParseRefreshToken(refreshAccessTokenDto.RefreshToken, out var claims))
            {
                return UnprocessableEntity();
            }

            var userId = claims.FindFirstValue(JwtRegisteredClaimNames.Sub);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return UnprocessableEntity("Invalid token");
            }

            if (user.ForceRelogin)
            {
                return UnprocessableEntity();
            }

            var roles = await _userManager.GetRolesAsync(user);

            var accessToken = _jwtTokenService.CreateAccessToken(user.Id, user.UserName, roles);
            var refreshToken = _jwtTokenService.CreateRefreshToken(user.Id);

            return Ok(new SuccessfulLoginDto(accessToken, refreshToken));
        }
    }
}
