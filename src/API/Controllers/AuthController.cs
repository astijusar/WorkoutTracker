using System.Security.Claims;
using API.Filters;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Repository.Models;
using Repository.Models.DTOs.JWT;
using Repository.Models.DTOs.User;

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

        [HttpPost("accessToken")]
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
