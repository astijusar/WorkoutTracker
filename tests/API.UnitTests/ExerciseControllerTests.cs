using API.Controllers;
using AutoMapper;
using Core.Models;
using Core.Models.DTOs.Exercise;
using Core.Models.Enums;
using Core.Models.RequestFeatures;
using Data.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace API.UnitTests
{
    public class ExerciseControllerTests
    {
        private readonly Mock<IRepositoryManager> _mockRepo;
        private ExerciseController _controller;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<ExerciseController>> _mockLogger;

        public ExerciseControllerTests()
        {
            _mockMapper = new();
            _mockLogger = new();
            _mockRepo = new Mock<IRepositoryManager>();
            _controller = new ExerciseController(_mockRepo.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetExercises_ReturnsOkResult_WithAListOfExercises()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.Exercise.GetAllExercisesPagedAsync(false, It.IsAny<ExerciseParameters>()))
                .ReturnsAsync(new OffsetPaginationResponse<Exercise>(GetTestExercises(), new OffsetPaginationMetadata(10, 1, 10)));

            // Act
            var result = await _controller.GetExercises(new ExerciseParameters(null, null, null));

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetExercise_ReturnsNotFoundResult_WhenExerciseDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.Exercise.GetExerciseAsync(It.IsAny<Guid>(), false))
                .ReturnsAsync((Exercise)null);

            // Act
            var result = await _controller.GetExercise(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateExercise_ReturnsCreatedAtRouteResult_WhenExerciseIsCreated()
        {
            // Arrange
            var exerciseCreationDto = new ExerciseCreationDto("Test", null, MuscleGroup.Abdominals.ToString(),
                Equipment.Band.ToString());
            var exerciseDto = new ExerciseDto(Guid.NewGuid(), "Test", "", MuscleGroup.Abdominals,
                Equipment.Band);
            var exercise = new Exercise
            {
                Instructions = null,
                EquipmentType = Equipment.Band,
                MuscleGroup = MuscleGroup.Abdominals,
                Name = "Test"
            };

            _mockRepo.Setup(repo => repo.Exercise.CreateExercise(It.IsAny<Exercise>()));
            _mockRepo.Setup(repo => repo.SaveAsync()).Returns(Task.FromResult(true));
            _mockMapper.Setup(mapper => mapper.Map<Exercise>(exerciseCreationDto)).Returns(exercise);
            _mockMapper.Setup(mapper => mapper.Map<ExerciseDto>(exercise)).Returns(exerciseDto);
            _controller = new ExerciseController(_mockRepo.Object, _mockMapper.Object, _mockLogger.Object);

            // Act
            var result = await _controller.CreateExercise(exerciseCreationDto);

            // Assert
            Assert.IsType<CreatedAtRouteResult>(result);
        }

        private static IEnumerable<Exercise> GetTestExercises()
        {
            var exercises = new List<Exercise>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Exercise 1",
                    Instructions = "Test Instructions 1",
                    MuscleGroup = MuscleGroup.Abdominals,
                    EquipmentType = Equipment.Barbell
                }
            };

            return exercises;
        }
    }
}
