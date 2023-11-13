using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Core.Exceptions;
using Core.Models;
using Data.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters
{
    public class WorkoutForUserExistsFilterAttribute : IAsyncActionFilter
    {
        private readonly ILogger<WorkoutForUserExistsFilterAttribute> _logger;
        private readonly IRepositoryManager _repository;
        private readonly UserManager<User> _userManager;

        public WorkoutForUserExistsFilterAttribute(ILogger<WorkoutForUserExistsFilterAttribute> logger,
            IRepositoryManager repository, UserManager<User> userManager)
        {
            _logger = logger;
            _repository = repository;
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

            var method = context.HttpContext.Request.Method;
            var trackChanges = method.Equals("PUT") || method.Equals("PATCH") || method.Equals("DELETE");
            var id = context.ActionArguments["workoutId"];
            
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id),
                    $"Argument '{nameof(id)}' is null in the action filter '{nameof(ExerciseExistsFilterAttribute)}'.");
            }

            if (!Guid.TryParse(id.ToString(), out var workoutId))
            {
                throw new InvalidGuidException(nameof(id));
            }

            var workout = await _repository.Workout.GetWorkoutAsync(userId, workoutId, trackChanges);

            if (workout == null)
            {
                _logger.LogWarning($"Workout with id: {workoutId} doesn't exist in the database.");
                context.Result = new NotFoundResult();

                return;
            }

            context.HttpContext.Items.Add("workout", workout);
            await next();
        }
    }
}
