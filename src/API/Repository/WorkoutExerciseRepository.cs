﻿using API.Models;
using API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class WorkoutExerciseRepository : RepositoryBase<WorkoutExercise>, IWorkoutExerciseRepository
    {
        public WorkoutExerciseRepository(ApplicationContext repositoryContext) 
            : base(repositoryContext)
        {
        }

        public async Task<IList<WorkoutExercise>> GetWorkoutExercisesAsync(Guid workoutId, bool trackChanges) =>
            await FindBy(e => e.WorkoutId == workoutId, trackChanges)
                .Include(e => e.Exercise)
                .Include(e => e.Sets)
                .ToListAsync();

        public async Task<IList<WorkoutExercise>> GetWorkoutExercisesAsyncByIds(Guid workoutId,
            IList<Guid> exerciseIds, bool trackChanges) =>
            await FindBy(e => e.WorkoutId == workoutId && exerciseIds.Contains(e.Id), trackChanges)
                .Include(e => e.Exercise)
                .ToListAsync();

        public async Task<WorkoutExercise?> GetWorkoutExerciseAsync(Guid workoutId, Guid exerciseId, bool trackChanges) =>
            await FindBy(e => e.WorkoutId == workoutId && e.Id == exerciseId, trackChanges)
                .Include(e => e.Exercise)
                .Include(e => e.Sets)
                .SingleOrDefaultAsync();

        public async Task CreateWorkoutExerciseAsync(Guid workoutId, WorkoutExercise exercise)
        {
            if (exercise.Sets.Any())
            {
                var n = 1;

                foreach (var set in exercise.Sets)
                {
                    set.WorkoutExerciseId = workoutId;
                    set.Order = n;
                    n++;
                }
            }

            var currentMaxOrder = await FindBy(e => e.WorkoutId == workoutId, false)
                .MaxAsync(e => (int?)e.Order) ?? 0;

            exercise.Order = currentMaxOrder + 1;
            exercise.WorkoutId = workoutId;

            Create(exercise);
        }

        public void DeleteWorkoutExercise(WorkoutExercise exercise)
        {
            var workoutExercises = 
                FindBy(e => e.WorkoutId == exercise.WorkoutId, true)
                    .OrderBy(e => e.Order)
                    .ToList();

            var indexToDelete = workoutExercises.FindIndex(e => e.Id == exercise.Id);

            if (indexToDelete < 0) return;

            Delete(exercise);

            for (var i = indexToDelete + 1; i < workoutExercises.Count; i++)
            {
                workoutExercises[i].Order--;
            }
        }
    }
}
