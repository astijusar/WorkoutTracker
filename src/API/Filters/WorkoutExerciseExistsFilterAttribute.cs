using API.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters
{
    public class WorkoutExerciseExistsFilterAttribute : IAsyncActionFilter
    {
        private readonly ILogger<WorkoutExerciseExistsFilterAttribute> _logger;
        private readonly IRepositoryManager _repository;

        public WorkoutExerciseExistsFilterAttribute(ILogger<WorkoutExerciseExistsFilterAttribute> logger, IRepositoryManager repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH"));
            var exerciseId = (Guid)context.ActionArguments["exerciseId"]!;
            var workoutId = (Guid)context.ActionArguments["workoutId"]!;

            // TODO: throw exception if id is null

            var exercise = await _repository.WorkoutExercise.GetWorkoutExerciseAsync(workoutId, exerciseId, trackChanges);

            if (exercise == null)
            {
                _logger.LogWarning($"Workout exercise with id: {exerciseId} doesn't exist in the database.");
                context.Result = new NotFoundResult();

                return;
            }

            context.HttpContext.Items.Add("workoutExercise", exercise);
            await next();
        }
    }
}
