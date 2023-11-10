using API.Filters;
using API.Models;
using API.Models.DTOs.Workout;
using API.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
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

        [HttpGet]
        [ServiceFilter(typeof(UserExistsFilterAttribute))]
        public async Task<IActionResult> GetWorkouts()
        {
            var user = HttpContext.Items["user"] as User;

            var workouts = await _repository.Workout.GetAllWorkoutsAsync(user!.Id, false);

            var workoutsDto = _mapper.Map<IEnumerable<WorkoutDto>>(workouts);

            return Ok(workoutsDto);
        }

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

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(UserExistsFilterAttribute))]
        public async Task<IActionResult> CreateWorkout([FromBody] WorkoutCreationDto input)
        {
            var user = HttpContext.Items["user"] as User;

            var workout = _mapper.Map<Workout>(input);

            _repository.Workout.CreateWorkout(user!.Id, workout);
            await _repository.SaveAsync();

            var workoutDto = _mapper.Map<WorkoutDto>(workout);

            return CreatedAtRoute("GetWorkout", new { workoutId = workoutDto.Id }, workoutDto);
        }

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
