using API.Models;
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
            var id = (Guid)context.ActionArguments["workoutId"]!;

            // TODO: throw exception if id is null

            var workout = await _repository.Workout.GetWorkoutAsync(id, trackChanges);

            if (workout == null)
            {
                _logger.LogWarning($"Workout with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();

                await next();
            }

            context.HttpContext.Items.Add("workout", workout);
            await next();
        }
    }
}
