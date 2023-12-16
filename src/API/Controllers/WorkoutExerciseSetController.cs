using API.Filters;
using AutoMapper;
using Core.Models;
using Core.Models.DTOs.WorkoutExerciseSet;
using Data.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/workout/{workoutId}/exercise/{exerciseId}/set")]
    [ApiController]
    public class WorkoutExerciseSetController : ControllerBase
    {
        private readonly ILogger<WorkoutExerciseSetController> _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public WorkoutExerciseSetController(ILogger<WorkoutExerciseSetController> logger, IRepositoryManager repository,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all exercise sets for a specific exercise
        /// </summary>
        /// <param name="workoutId">Workout id</param>
        /// <param name="exerciseId">Exercise id</param>
        /// <returns>A list of exercise sets</returns>
        /// <response code="200">Returns a list of exercise sets</response>
        /// <response code="404">Exercise or workout does not exist</response>
        [HttpGet]
        [ServiceFilter(typeof(WorkoutExerciseExistsFilterAttribute))]
        public IActionResult GetExerciseSets(Guid workoutId, Guid exerciseId)
        {
            var exercise = HttpContext.Items["workoutExercise"] as WorkoutExercise;
            var exerciseSets = exercise!.Sets;

            var exerciseSetsDto = _mapper.Map<IEnumerable<WorkoutExerciseSetDto>>(exerciseSets);

            return Ok(exerciseSetsDto);
        }

        /// <summary>
        /// Get a specific exercise set
        /// </summary>
        /// <param name="workoutId">Workout id</param>
        /// <param name="exerciseId">Exercise id</param>
        /// <param name="setId">Exercise set id</param>
        /// <returns>An exercise set</returns>
        /// <response code="200">Returns an exercise set</response>
        /// <response code="404">Exercise set, exercise, or workout does not exist</response>
        [HttpGet("{setId:guid}", Name = "GetExerciseSet")]
        [ServiceFilter(typeof(WorkoutExerciseExistsFilterAttribute))]
        public IActionResult GetExerciseSet(Guid workoutId, Guid exerciseId, Guid setId)
        {
            var exercise = HttpContext.Items["workoutExercise"] as WorkoutExercise;
            var exerciseSet = exercise!.Sets.SingleOrDefault(s => s.Id == setId);

            if (exerciseSet == null)
            {
                return NotFound();
            }

            var exerciseSetDto = _mapper.Map<WorkoutExerciseSetDto>(exerciseSet);

            return Ok(exerciseSetDto);
        }

        /// <summary>
        /// Create a new exercise set
        /// </summary>
        /// <param name="workoutId">Workout id</param>
        /// <param name="exerciseId">Exercise id</param>
        /// <param name="input">Exercise set creation object</param>
        /// <returns>A newly created exercise set</returns>
        /// <response code="201">Returns a newly created exercise set</response>
        /// <response code="400">Exercise set creation object sent from client is null</response>
        /// <response code="422">Invalid model state for the exercise set creation object</response>
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(WorkoutExerciseExistsFilterAttribute))]
        public async Task<IActionResult> CreateExerciseSet(Guid workoutId, Guid exerciseId, [FromBody] WorkoutExerciseSetCreationDto input)
        {
            var exercise = HttpContext.Items["workoutExercise"] as WorkoutExercise;
            var exerciseSet = _mapper.Map<WorkoutExerciseSet>(input);

            exercise!.Sets.Add(exerciseSet);
            await _repository.SaveAsync();

            var exerciseSetDto = _mapper.Map<WorkoutExerciseSetDto>(exerciseSet);

            return CreatedAtRoute("GetExerciseSet", new { workoutId, exerciseId, setId = exerciseSetDto.Id }, exerciseSetDto);
        }

        /// <summary>
        /// Update an exercise set
        /// </summary>
        /// <param name="workoutId">Workout id</param>
        /// <param name="exerciseId">Exercise id</param>
        /// <param name="setId">Exercise set id</param>
        /// <param name="input">Exercise set update object</param>
        /// <returns>204 no content response</returns>
        /// <response code="204">No content response</response>
        /// <response code="400">Exercise set update object is null</response>
        /// <response code="422">Invalid model state for the exercise set update object</response>
        /// <response code="404">Exercise set, exercise, or workout is not found</response>
        [HttpPut("{setId:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(WorkoutExerciseExistsFilterAttribute))]
        public async Task<IActionResult> UpdateSet(Guid workoutId, Guid exerciseId, Guid setId,
            [FromBody] WorkoutExerciseSetUpdateDto input)
        {
            var exercise = HttpContext.Items["workoutExercise"] as WorkoutExercise;
            var exerciseSet = exercise!.Sets.SingleOrDefault(s => s.Id == setId);

            if (exerciseSet == null)
            {
                return NotFound();
            }

            _mapper.Map(input, exerciseSet);
            await _repository.SaveAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete an exercise set
        /// </summary>
        /// <param name="workoutId">Workout id</param>
        /// <param name="exerciseId">Exercise id</param>
        /// <param name="setId">Exercise set id</param>
        /// <returns>204 no content response</returns>
        /// <response code="204">No content response</response>
        /// <response code="404">Exercise set, exercise, or workout is not found</response>
        [HttpDelete("{setId:guid}")]
        [ServiceFilter(typeof(WorkoutExerciseExistsFilterAttribute))]
        public async Task<IActionResult> DeleteExerciseSet(Guid workoutId, Guid exerciseId, Guid setId)
        {
            var exercise = HttpContext.Items["workoutExercise"] as WorkoutExercise;
            var exerciseSet = exercise!.Sets.SingleOrDefault(s => s.Id == setId);

            if (exerciseSet == null)
            {
                return NotFound();
            }

            _repository.WorkoutExerciseSet.DeleteExerciseSet(exerciseSet);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
