using API.Filters;
using AutoMapper;
using Core.Models;
using Core.Models.DTOs.Workout;
using Core.Models.RequestFeatures;
using Data.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly ILogger<WorkoutController> _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public WorkoutController(ILogger<WorkoutController> logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all of the workouts
        /// </summary>
        /// <returns>A list of all workouts</returns>
        /// <response code="200">Returns a list of all workouts</response>
        [HttpGet]
        [ServiceFilter(typeof(UserExistsFilterAttribute))]
        public async Task<IActionResult> GetWorkouts([FromQuery] WorkoutParameters parameters)
        {
            var user = HttpContext.Items["user"] as User;

            var workouts = await _repository.Workout.GetAllWorkoutsAsync(user!.Id, false, parameters);

            var workoutsDto = _mapper.Map<IEnumerable<WorkoutDto>>(workouts.Data);

            return Ok(new OffsetPaginationResponse<WorkoutDto>(workoutsDto, workouts.Pagination));
        }

        /// <summary>
        /// Get a workout by id
        /// </summary>
        /// <param name="workoutId">Workout to return id</param>
        /// <returns>A workout by id</returns>
        /// <response code="200">Returns a workout by id</response>
        /// <response code="404">Workout does not exist</response>
        [HttpGet("{workoutId:guid}", Name = "GetWorkout")]
        [ServiceFilter(typeof(UserExistsFilterAttribute))]
        public async Task<IActionResult> GetWorkout(Guid workoutId)
        {
            var user = HttpContext.Items["user"] as User;

            var workout = await _repository.Workout
                .GetWorkoutAsync(user!.Id, workoutId, false);

            if (workout == null)
            {
                _logger.LogWarning($"Workout with id: {workoutId} doesn't exist in the database");
                return NotFound();
            }

            var workoutDto = _mapper.Map<WorkoutDto>(workout);

            return Ok(workoutDto);
        }

        /// <summary>
        /// Create a new workout
        /// </summary>
        /// <param name="input">Workout creation object</param>
        /// <returns>A newly created workout</returns>
        /// <response code="201">Returns a newly created workout</response>
        /// <response code="400">Workout creation object sent from client is null</response>
        /// <response code="422">Invalid model state for the workout creation object</response>
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(UserExistsFilterAttribute))]
        public async Task<IActionResult> CreateWorkout([FromBody] WorkoutCreationDto input)
        {
            var user = HttpContext.Items["user"] as User;

            var workout = _mapper.Map<Workout>(input);
            workout.Start = workout.Start?.ToUniversalTime();
            workout.End = workout.End?.ToUniversalTime();

            _repository.Workout.CreateWorkout(user!.Id, workout);
            await _repository.SaveAsync();

            var workoutDto = _mapper.Map<WorkoutDto>(workout);

            return CreatedAtRoute("GetWorkout", new { workoutId = workoutDto.Id }, workoutDto);
        }

        /// <summary>
        /// Update workout by id
        /// </summary>
        /// <param name="workoutId">Workout to be updated id</param>
        /// <param name="input">Workout update object</param>
        /// <returns>204 no content response</returns>
        /// <response code="204">No content response</response>
        /// <response code="400">Workout update object is null</response>
        /// <response code="422">Invalid model state for the workout update object</response>
        /// <response code="404">Workout is not found</response>
        [HttpPut("{workoutId:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(WorkoutForUserExistsFilterAttribute))]
        public async Task<IActionResult> UpdateWorkout(Guid workoutId, [FromBody] WorkoutUpdateDto input)
        {
            var workout = HttpContext.Items["workout"] as Workout;

            _mapper.Map(input, workout);
            await _repository.SaveAsync();

            return NoContent();
        }

        /// <summary>
        /// Partially update workout by id
        /// </summary>
        /// <param name="workoutId">Workout to be updated id</param>
        /// <param name="patchInput">Workout patch object</param>
        /// <returns>204 no content response</returns>
        /// <response code="204">No content response</response>
        /// <response code="400">Workout patch object is null</response>
        /// <response code="422">Invalid model state for the workout patch object</response>
        /// <response code="404">Workout is not found</response>
        [HttpPatch("{workoutId:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(WorkoutForUserExistsFilterAttribute))]
        public async Task<IActionResult> PartiallyUpdateWorkout(Guid workoutId,
            [FromBody] JsonPatchDocument<WorkoutUpdateDto> patchInput)
        {
            if (patchInput == null)
            {
                _logger.LogWarning("Workout patch object is null.");
                return BadRequest("Workout patch object is null");
            }

            var workout = HttpContext.Items["workout"] as Workout;

            var workoutToPatch = _mapper.Map<WorkoutUpdateDto>(workout);

            patchInput.ApplyTo(workoutToPatch);

            TryValidateModel(workoutToPatch);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for the workout patch object.");
                return UnprocessableEntity(ModelState);
            }

            _mapper.Map(workoutToPatch, workout);

            await _repository.SaveAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete workout by id
        /// </summary>
        /// <param name="workoutId">Workout to be deleted id</param>
        /// <returns>204 no context response</returns>
        /// <response code="204">No content response</response>
        /// <response code="404">Workout is not found</response>
        [HttpDelete("{workoutId:guid}")]
        [ServiceFilter(typeof(WorkoutForUserExistsFilterAttribute))]
        public async Task<IActionResult> DeleteWorkout(Guid workoutId)
        {
            var workout = HttpContext.Items["workout"] as Workout;

            _repository.Workout.DeleteWorkout(workout!);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
