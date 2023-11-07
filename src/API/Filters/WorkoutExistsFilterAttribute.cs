using API.Exceptions;
using API.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters
{
    public class WorkoutExistsFilterAttribute : IAsyncActionFilter
    {
        private readonly ILogger<WorkoutExistsFilterAttribute> _logger;
        private readonly IRepositoryManager _repository;

        public WorkoutExistsFilterAttribute(ILogger<WorkoutExistsFilterAttribute> logger,
            IRepositoryManager repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = method.Equals("PUT") || method.Equals("PATCH");
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

            var workout = await _repository.Workout.GetWorkoutAsync(workoutId, trackChanges);

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
