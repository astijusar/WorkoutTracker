using Core.Models;
using Core.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Data.Repository.Seeders
{
    public class DbSeeder
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<DbSeeder> _logger;

        public DbSeeder(ApplicationContext context, UserManager<User> userManager, ILogger<DbSeeder> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            await AddExercises();
            await AddDemoWorkout();
        }

        private async Task AddExercises()
        {
            var existingExerciseNames = _context.Exercises.Select(e => e.Name).ToList();
            var newExercises = ExerciseConstants.Exercises
                .Where(exercise => !existingExerciseNames.Contains(exercise.Name))
                .ToList();

            if (newExercises.Any())
            {
                await _context.Exercises.AddRangeAsync(newExercises);
                await _context.SaveChangesAsync();
            }
        }

        private async Task AddDemoWorkout()
        {
            var workoutExists = _context.Workouts.FirstOrDefault(w => w.Name == "demoWorkout");

            if (workoutExists != null)
                return;

            var demoUser = await _userManager.FindByNameAsync("demoUser");

            if (demoUser == null)
            {
                _logger.LogError("Demo user does not exist when creating a demo workout!");
                return;
            }

            var demoWorkout = new Workout
            {
                Name = "demoWorkout",
                IsTemplate = false,
                User = demoUser,
                Exercises = new List<WorkoutExercise>
                {
                    new()
                    {
                        ExerciseId = ExerciseConstants.GetExerciseId(0),
                        Order = 1,
                        Sets = new List<WorkoutExerciseSet>
                        {
                            new()
                            {
                                Order = 1,
                                Reps = 10,
                                Weight = 10,
                                MeasurementType = MeasurementType.Kilograms
                            },
                            new()
                            {
                                Order = 2,
                                Reps = 10,
                                Weight = 30,
                                MeasurementType = MeasurementType.Kilograms
                            }
                        }
                    },
                    new()
                    {
                        ExerciseId = ExerciseConstants.GetExerciseId(1),
                        Order = 2,
                        Sets = new List<WorkoutExerciseSet>
                        {
                            new()
                            {
                                Order = 1,
                                Reps = 20,
                                Weight = 5,
                                MeasurementType = MeasurementType.Kilograms
                            },
                            new()
                            {
                                Order = 2,
                                Reps = 15,
                                Weight = 5,
                                MeasurementType = MeasurementType.Kilograms
                            }
                        }
                    },
                    new()
                    {
                        ExerciseId = ExerciseConstants.GetExerciseId(4),
                        Order = 3,
                        Sets = new List<WorkoutExerciseSet>
                        {
                            new()
                            {
                                Order = 1,
                                Reps = 10,
                                Weight = 12,
                                MeasurementType = MeasurementType.Kilograms
                            },
                            new()
                            {
                                Order = 2,
                                Reps = 2,
                                Weight = 30,
                                MeasurementType = MeasurementType.Kilograms
                            }
                        }
                    }
                }
            };

            _context.Workouts.Add(demoWorkout);
            await _context.SaveChangesAsync();
        }
    }
}
