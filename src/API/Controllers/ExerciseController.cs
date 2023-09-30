using API.Models.DTOs;
using API.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public ExerciseController(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetExercises()
        {
            var exercises = await _repository.Exercise.GetAllExercisesAsync(false);

            var exercisesDto = _mapper.Map<List<ExerciseDto>>(exercises);

            return Ok(exercisesDto);
        }
    }
}
