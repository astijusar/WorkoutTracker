﻿using API.Filters;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Repository.Models;
using Repository.Models.DTOs.WorkoutExerciseSet;

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
        public async Task<IActionResult> GetExerciseSets(Guid workoutId, Guid exerciseId)
        {
            var exerciseSets = await _repository.WorkoutExerciseSet.GetExerciseSetsAsync(exerciseId, false);

            var exerciseSetsDto = _mapper.Map<IEnumerable<WorkoutExerciseSetDto>>(exerciseSets);

            return Ok(exerciseSetsDto);
        }

        [HttpGet("{setId:guid}", Name = "GetExerciseSet")]
        public async Task<IActionResult> GetExerciseSet(Guid workoutId, Guid exerciseId, Guid setId)
        {
            var exerciseSet = await _repository.WorkoutExerciseSet.GetExerciseSetAsync(exerciseId, setId, false);

            if (exerciseSet == null)
            {
                return NotFound();
            }

            var exerciseSetDto = _mapper.Map<WorkoutExerciseSetDto>(exerciseSet);

            return Ok(exerciseSetDto);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateExerciseSet(Guid workoutId, Guid exerciseId, [FromBody] WorkoutExerciseSetCreationDto input)
        {
            var exerciseSet = _mapper.Map<WorkoutExerciseSet>(input);

            await _repository.WorkoutExerciseSet.CreateExerciseSetAsync(exerciseId, exerciseSet);
            await _repository.SaveAsync();

            var exerciseSetDto = _mapper.Map<WorkoutExerciseSetDto>(exerciseSet);

            return CreatedAtRoute("GetExerciseSet", new { workoutId, exerciseId, setId = exerciseSetDto.Id }, exerciseSetDto);
        }

        [HttpPut("{setId:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateSet(Guid workoutId, Guid exerciseId, Guid setId,
            [FromBody] WorkoutExerciseSetUpdateDto input)
        {
            var exercise = await _repository.WorkoutExerciseSet.GetExerciseSetAsync(exerciseId, setId, true);

            if (exercise == null)
            {
                return NotFound();
            }

            _mapper.Map(input, exercise);
            await _repository.SaveAsync();

            return NoContent();
        }


        [HttpDelete("{setId:guid}")]
        public async Task<IActionResult> DeleteExerciseSet(Guid workoutId, Guid exerciseId, Guid setId)
        {
            var exerciseSet = await _repository.WorkoutExerciseSet.GetExerciseSetAsync(exerciseId, setId, true);

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
