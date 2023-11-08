using System.Collections.ObjectModel;
using API.Models;
using API.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace API.Repository.Seeders
{
    public class DbSeeder
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<DbSeeder> _logger;
        private static readonly List<Exercise> Exercises = new()
        {
            new()
            {
                Id = new Guid("e43f505f-abb1-491d-a75b-cc0dad3d998d"),
                Name = "Bench press",
                MuscleGroup = MuscleGroup.Chest,
                EquipmentType = Equipment.Barbell
            },
            new()
            {
                Id = new Guid("358ba0f0-fc8e-4243-a33d-61b3b3c242ff"),
                Name = "Shoulder press",
                MuscleGroup = MuscleGroup.Shoulders,
                EquipmentType = Equipment.Barbell
            },
            new()
            {
                Id = new Guid("fa7c0176-4c6c-4f8c-b383-8f60c35456f8"),
                Name = "Shoulder press",
                MuscleGroup = MuscleGroup.Shoulders,
                EquipmentType = Equipment.Dumbbell
            },
            new()
            {
                Id = new Guid("a1ce7a99-1d8f-41c6-98cc-f7ea90584c9a"),
                Name = "Triceps pushdown",
                MuscleGroup = MuscleGroup.Triceps,
                EquipmentType = Equipment.Cable
            },
            new()
            {
                Id = new Guid("1dfdc7a8-5a99-4b23-9a0b-0486b38a362e"),
                Name = "Bicep curl",
                MuscleGroup = MuscleGroup.Biceps,
                EquipmentType = Equipment.Barbell
            },
            new()
            {
                Id = new Guid("78531826-4f87-4b35-b2dc-c52b44e3dfa2"),
                Name = "Bicep curl",
                MuscleGroup = MuscleGroup.Biceps,
                EquipmentType = Equipment.Dumbbell
            },
            new()
            {
                Id = new Guid("fd636ca6-8380-4cfd-8ef2-c4094438999f"),
                Name = "Bicep curl",
                MuscleGroup = MuscleGroup.Biceps,
                EquipmentType = Equipment.Cable
            },
            new()
            {
                Id = new Guid("e737464c-348e-472d-986b-197fba819b1b"),
                Name = "Lat pulldown",
                MuscleGroup = MuscleGroup.Lats,
                EquipmentType = Equipment.Cable
            },
            new()
            {
                Id = new Guid("bb3b5607-b000-4a46-8ad9-8ad92fb07f71"),
                Name = "Crunches",
                MuscleGroup = MuscleGroup.Abdominals,
                EquipmentType = Equipment.Bodyweight
            },
        };

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
            if (_context.Exercises.Any())
                return;

            await _context.Exercises.AddRangeAsync(Exercises);
            await _context.SaveChangesAsync();
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
                IsTemplate = true,
                User = demoUser,
                Exercises = new List<WorkoutExercise>
                {
                    new()
                    {
                        ExerciseId = new Guid("e43f505f-abb1-491d-a75b-cc0dad3d998d"),
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
                    }
                }
            };

            _context.Workouts.Add(demoWorkout);
            await _context.SaveChangesAsync();
        }
    }
}
