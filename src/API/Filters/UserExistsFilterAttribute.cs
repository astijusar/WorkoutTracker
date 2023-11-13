using System.Security.Claims;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.JsonWebTokens;

namespace API.Filters
{
    public class UserExistsFilterAttribute : IAsyncActionFilter
    {
        private readonly UserManager<User> _userManager;

        public UserExistsFilterAttribute(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userId = context.HttpContext.User
                .FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (string.IsNullOrEmpty(userId))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                context.Result = new NotFoundResult();
                return;
            }

            context.HttpContext.Items.Add("user", user);
            await next();
        }
    }
}
