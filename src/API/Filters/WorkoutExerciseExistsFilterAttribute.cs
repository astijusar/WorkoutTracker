﻿using Core.Exceptions;
using Data.Repository.Interfaces;
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
            var trackChanges = method.Equals("PUT") || method.Equals("PATCH") || method.Equals("DELETE");

            var exerciseId = CheckAndParseGuid(context.ActionArguments["exerciseId"]);
            var workoutId = CheckAndParseGuid(context.ActionArguments["workoutId"]);

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

        private Guid CheckAndParseGuid(object? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id),
                    $"Argument '{nameof(id)}' is null in the action filter '{nameof(WorkoutExerciseExistsFilterAttribute)}'.");
            }

            if (!Guid.TryParse(id.ToString(), out var guid))
            {
                throw new InvalidGuidException(nameof(id));
            }

            return guid;
        }
    }
}
