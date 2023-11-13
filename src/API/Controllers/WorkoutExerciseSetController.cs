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

        [HttpGet]
        [ServiceFilter(typeof(WorkoutExerciseExistsFilterAttribute))]
        public IActionResult GetExerciseSets(Guid workoutId, Guid exerciseId)
        {
            var exercise = HttpContext.Items["workoutExercise"] as WorkoutExercise;
            var exerciseSets = exercise!.Sets;

            var exerciseSetsDto = _mapper.Map<IEnumerable<WorkoutExerciseSetDto>>(exerciseSets);

            return Ok(exerciseSetsDto);
        }

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
