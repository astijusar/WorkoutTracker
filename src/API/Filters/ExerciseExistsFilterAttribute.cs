using API.Repository.Interfaces;
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
            var trackChanges = method.Equals("PUT") || method.Equals("PATCH");
            var id = (Guid)context.ActionArguments["exerciseId"]!;

            // TODO: throw exception if id is null

            var exercise = await _repository.Exercise.GetExerciseAsync(id, trackChanges);

            if (exercise == null)
            {
                _logger.LogWarning($"Exercise with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();

                return;
            }

            context.HttpContext.Items.Add("exercise", exercise);
            await next();
        }
    }
}
