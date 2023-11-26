using Core.Models;
using Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
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

        public async Task CreateWorkoutExercisesAsync(Guid workoutId, IList<WorkoutExercise> exercises)
        {
            foreach (var exercise in exercises)
            {
                if (!exercise.Sets.Any()) continue;

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

            for (var i = 0; i < exercises.Count; i++)
            {
                exercises[i].Order = currentMaxOrder + i + 1;
                exercises[i].WorkoutId = workoutId;
                Create(exercises[i]);
            }
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
