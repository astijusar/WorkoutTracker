﻿using API.Filters;
using API.Models;
using API.Models.DTOs.Exercise;
using API.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ExerciseController> _logger;

        public ExerciseController(IRepositoryManager repository, IMapper mapper, ILogger<ExerciseController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all of the exercises
        /// </summary>
        /// <returns>A list of all exercises</returns>
        /// <response code="200">Returns a list of all exercises</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetExercises()
        {
            var exercises = await _repository.Exercise.GetAllExercisesAsync(false);

            var exercisesDto = _mapper.Map<List<ExerciseDto>>(exercises);

            return Ok(exercisesDto);
        }

        /// <summary>
        /// Get an exercise by id
        /// </summary>
        /// <param name="exerciseId">Exercise to return id</param>
        /// <returns>An exercise by id</returns>
        /// <response code="200">Returns an exercise by id</response>
        /// <response code="404">Exercise does not exist</response>
        [HttpGet("{exerciseId:guid}", Name = "GetExercise")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetExercise(Guid exerciseId)
        {
            var exercise = await _repository.Exercise
                .GetExerciseAsync(exerciseId, false);

            if (exercise == null)
            {
                _logger.LogWarning($"Exercise with id: {exerciseId} doesn't exist in the database");
                return NotFound();
            }

            var exerciseDto = _mapper.Map<ExerciseDto>(exercise);

            return Ok(exerciseDto);
        }


        /// <summary>
        /// Create a new exercise
        /// </summary>
        /// <param name="input">Exercise creation object</param>
        /// <returns>A newly created exercise</returns>
        /// <response code="201">Returns a newly created exercise</response>
        /// <response code="400">Exercise creation object sent from client is null</response>
        /// <response code="422">Invalid model state for the exercise creation object</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateExercise([FromBody] ExerciseCreationDto input)
        {
            var exercise = _mapper.Map<Exercise>(input);

            _repository.Exercise.CreateExercise(exercise);
            await _repository.SaveAsync();

            var exerciseDto = _mapper.Map<ExerciseDto>(exercise);

            return CreatedAtRoute("GetExercise", new { exerciseId = exerciseDto.Id }, exerciseDto);
        }

        /// <summary>
        /// Update exercise by id
        /// </summary>
        /// <param name="exerciseId">Exercise to be updated id</param>
        /// <param name="input">Exercise update object</param>
        /// <returns>204 no content response</returns>
        /// <response code="204">No content response</response>
        /// <response code="400">Exercise update object is null</response>
        /// <response code="422">Invalid model state for the exercise update object</response>
        /// <response code="404">Exercise is not found</response>
        [HttpPut("{exerciseId:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(404)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ExerciseExistsValidationFilterAttribute))]
        public async Task<IActionResult> UpdateExercise(Guid exerciseId, [FromBody] ExerciseUpdateDto input)
        {
            var exercise = HttpContext.Items["exercise"] as Exercise;

            _mapper.Map(input, exercise);
            await _repository.SaveAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete exercise by id
        /// </summary>
        /// <param name="exerciseId">Exercise to be deleted id</param>
        /// <returns>204 no context response</returns>
        /// <response code="204">No content response</response>
        /// <response code="404">Exercise is not found</response>
        [HttpDelete("{exerciseId:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ServiceFilter(typeof(ExerciseExistsValidationFilterAttribute))]
        public async Task<IActionResult> DeleteExercise(Guid exerciseId)
        {
            var exercise = HttpContext.Items["exercise"] as Exercise;

            _repository.Exercise.DeleteExercise(exercise!);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
