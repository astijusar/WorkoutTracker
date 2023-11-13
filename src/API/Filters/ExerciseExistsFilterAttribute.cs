using Core.Exceptions;
using Data.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters
{
    public class ExerciseExistsFilterAttribute : IAsyncActionFilter
    {
        private readonly ILogger<ExerciseExistsFilterAttribute> _logger;
        private readonly IRepositoryManager _repository;

        public ExerciseExistsFilterAttribute(ILogger<ExerciseExistsFilterAttribute> logger,
            IRepositoryManager repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = method.Equals("PUT") || method.Equals("PATCH") || method.Equals("DELETE");
            var id = context.ActionArguments["exerciseId"];

            if (id == null)
            {
                throw new ArgumentNullException(nameof(id),
                    $"Argument '{nameof(id)}' is null in the action filter '{nameof(ExerciseExistsFilterAttribute)}'.");
            }

            if (!Guid.TryParse(id.ToString(), out var exerciseId))
            {
                throw new InvalidGuidException(nameof(id));
            }

            var exercise = await _repository.Exercise.GetExerciseAsync(exerciseId, trackChanges);

            if (exercise == null)
            {
                _logger.LogWarning($"Exercise with id: {exerciseId} doesn't exist in the database.");
                context.Result = new NotFoundResult();

                return;
            }

            context.HttpContext.Items.Add("exercise", exercise);
            await next();
        }
    }
}
