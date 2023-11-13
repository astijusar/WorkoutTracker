using API.Filters;
using API.Models;
using API.Models.DTOs.WorkoutExercise;
using API.Repository.Interfaces;
using AutoMapper;
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

        [HttpGet]
        [ServiceFilter(typeof(WorkoutForUserExistsFilterAttribute))]
        public IActionResult GetExercises(Guid workoutId)
        {
            var workout = HttpContext.Items["workout"] as Workout;

            var exercises = workout!.Exercises;

            var exercisesDto = _mapper.Map<IEnumerable<WorkoutExerciseDto>>(exercises);

            return Ok(exercisesDto);
        }

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

        [HttpPost("collection")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(WorkoutForUserExistsFilterAttribute))]
        public async Task<IActionResult> CreateExerciseCollection(Guid workoutId, [FromBody] IEnumerable<WorkoutExerciseCreationDto> input)
        {
            var exercises = _mapper.Map<IEnumerable<WorkoutExercise>>(input);

            foreach (var exercise in exercises)
            {
                await _repository.WorkoutExercise.CreateWorkoutExerciseAsync(workoutId, exercise);
            }

            await _repository.SaveAsync();

            var exercisesDto = _mapper.Map<IEnumerable<WorkoutExerciseDto>>(exercises);

            return CreatedAtAction("CreateExerciseCollection", exercisesDto);
        }

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
