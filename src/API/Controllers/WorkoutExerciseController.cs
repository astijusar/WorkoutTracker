using API.Filters;
using AutoMapper;
using Core.Models;
using Core.Models.DTOs.WorkoutExercise;
using Data.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/workout/{workoutId}/exercise")]
    [ApiController]
    public class WorkoutExerciseController : ControllerBase
    {
        private readonly ILogger<WorkoutExerciseController> _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public WorkoutExerciseController(ILogger<WorkoutExerciseController> logger, IRepositoryManager repository,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all exercises for a specific workout
        /// </summary>
        /// <param name="workoutId">Workout id</param>
        /// <returns>A list of all exercises for the workout</returns>
        /// <response code="200">Returns a list of all exercises for the workout</response>
        [HttpGet]
        [ServiceFilter(typeof(WorkoutForUserExistsFilterAttribute))]
        public IActionResult GetExercises(Guid workoutId)
        {
            var workout = HttpContext.Items["workout"] as Workout;

            var exercises = workout!.Exercises;

            var exercisesDto = _mapper.Map<IEnumerable<WorkoutExerciseDto>>(exercises);

            return Ok(exercisesDto);
        }

        /// <summary>
        /// Get a specific exercise for a workout by id
        /// </summary>
        /// <param name="workoutId">Workout id</param>
        /// <param name="exerciseId">Exercise id</param>
        /// <returns>The exercise for the workout</returns>
        /// <response code="200">Returns the exercise for the workout</response>
        /// <response code="404">Exercise not found</response>
        [HttpGet("{exerciseId:guid}", Name = "GetWorkoutExercise")]
        [ServiceFilter(typeof(WorkoutForUserExistsFilterAttribute))]
        public IActionResult GetExercise(Guid workoutId, Guid exerciseId)
        {
            var workout = HttpContext.Items["workout"] as Workout;

            var exercise = workout!.Exercises.SingleOrDefault(e => e.Id == exerciseId);

            if (exercise == null)
            {
                return NotFound();
            }

            var exerciseDto = _mapper.Map<WorkoutExerciseDto>(exercise);

            return Ok(exerciseDto);
        }

        /// <summary>
        /// Create a new exercise for a workout
        /// </summary>
        /// <param name="workoutId">Workout id</param>
        /// <param name="input">Exercise creation object</param>
        /// <returns>The newly created exercise</returns>
        /// <response code="201">Returns the newly created exercise</response>
        /// <response code="400">Exercise creation object is null</response>
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(WorkoutForUserExistsFilterAttribute))]
        public async Task<IActionResult> CreateExercise(Guid workoutId, [FromBody] WorkoutExerciseCreationDto input)
        {
            var workout = HttpContext.Items["workout"] as Workout;
            var exercise = _mapper.Map<WorkoutExercise>(input);

            workout!.Exercises.Add(exercise);
            await _repository.SaveAsync();

            var exerciseDto = _mapper.Map<WorkoutExerciseDto>(exercise);

            return CreatedAtRoute("GetWorkoutExercise", new { workoutId, exerciseId = exerciseDto.Id }, exerciseDto);
        }

        /// <summary>
        /// Create a collection of exercises for a workout
        /// </summary>
        /// <param name="workoutId">Workout id</param>
        /// <param name="input">Exercise creation objects</param>
        /// <returns>The created exercises</returns>
        /// <response code="201">Returns the created exercises</response>
        /// <response code="400">Exercise creation objects are null</response>
        [HttpPost("collection")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(WorkoutForUserExistsFilterAttribute))]
        public async Task<IActionResult> CreateExerciseCollection(Guid workoutId, [FromBody] IEnumerable<WorkoutExerciseCreationDto> input)
        {
            var exercises = _mapper.Map<IEnumerable<WorkoutExercise>>(input);

            await _repository.WorkoutExercise.CreateWorkoutExercisesAsync(workoutId, exercises.ToList());

            await _repository.SaveAsync();

            var exercisesDto = _mapper.Map<IEnumerable<WorkoutExerciseDto>>(exercises);

            return CreatedAtAction("CreateExerciseCollection", exercisesDto);
        }

        /// <summary>
        /// Update an exercise for a workout by id
        /// </summary>
        /// <param name="workoutId">Workout id</param>
        /// <param name="exerciseId">Exercise id</param>
        /// <param name="input">Exercise update object</param>
        /// <returns>No content response</returns>
        /// <response code="204">No content response</response>
        /// <response code="400">Exercise update object is null</response>
        /// <response code="404">Exercise not found</response>
        [HttpPut("{exerciseId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(WorkoutExerciseExistsFilterAttribute))]
        public async Task<IActionResult> UpdateExercises(Guid workoutId, Guid exerciseId,
            [FromBody] WorkoutExerciseUpdateDto input)
        {
            var exercise = HttpContext.Items["workoutExercise"] as WorkoutExercise;

            _mapper.Map(input, exercise);
            await _repository.SaveAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete an exercise for a workout by id
        /// </summary>
        /// <param name="workoutId">Workout id</param>
        /// <param name="exerciseId">Exercise id</param>
        /// <returns>No content response</returns>
        /// <response code="204">No content response</response>
        /// <response code="404">Exercise not found</response>
        [HttpDelete("{exerciseId:guid}")]
        [ServiceFilter(typeof(WorkoutExerciseExistsFilterAttribute))]
        public async Task<IActionResult> DeleteExercise(Guid workoutId, Guid exerciseId)
        {
            var exercise = HttpContext.Items["workoutExercise"] as WorkoutExercise;

            _repository.WorkoutExercise.DeleteWorkoutExercise(exercise!);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
