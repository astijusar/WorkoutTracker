using API.Models;
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

        public async Task<IEnumerable<WorkoutExercise>> GetWorkoutExercisesAsync(Guid workoutId, bool trackChanges) =>
            await FindBy(e => e.WorkoutId == workoutId, trackChanges)
                .Include(e => e.Exercise)
                .Include(e => e.WorkoutExerciseSets)
                .ToListAsync();

        public async Task<IEnumerable<WorkoutExercise>> GetWorkoutExercisesAsyncByIds(Guid workoutId,
            IEnumerable<Guid> exerciseIds, bool trackChanges) =>
            await FindBy(e => e.WorkoutId == workoutId && exerciseIds.Contains(e.Id), trackChanges)
                .Include(e => e.Exercise)
                .ToListAsync();

        public async Task<WorkoutExercise?> GetWorkoutExerciseAsync(Guid workoutId, Guid exerciseId, bool trackChanges) =>
            await FindBy(e => e.WorkoutId == workoutId && e.Id == exerciseId, trackChanges)
                .SingleOrDefaultAsync();

        public async Task CreateWorkoutExerciseAsync(Guid workoutId, WorkoutExercise exercise)
        {
            if (exercise.WorkoutExerciseSets.Any())
            {
                var n = 0;

                foreach (var set in exercise.WorkoutExerciseSets)
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

        public void DeleteWorkoutExercise(WorkoutExercise exercise) => Delete(exercise);
    }
}
