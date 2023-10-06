using API.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters
{
    public class ExerciseExistsValidationFilterAttribute : IAsyncActionFilter
    {
        private readonly ILogger<ExerciseExistsValidationFilterAttribute> _logger;
        private readonly IRepositoryManager _repository;

        public ExerciseExistsValidationFilterAttribute(ILogger<ExerciseExistsValidationFilterAttribute> logger,
            IRepositoryManager repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = method.Equals("PUT") || method.Equals("PATCH") ? true : false;
            var id = (Guid)context.ActionArguments["exerciseId"]!;

            var exercise = await _repository.Exercise.GetExerciseAsync(id, trackChanges);

            if (exercise == null)
            {
                _logger.LogWarning($"Exercise with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();

                await next();
            }

            context.HttpContext.Items.Add("exercise", exercise);
            await next();
        }
    }
}
