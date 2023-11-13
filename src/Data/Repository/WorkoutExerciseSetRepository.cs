using Core.Models;
using Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class WorkoutExerciseSetRepository : RepositoryBase<WorkoutExerciseSet>, IWorkoutExerciseSetRepository
    {
        public WorkoutExerciseSetRepository(ApplicationContext repositoryContext) 
            : base(repositoryContext)
        {
        }

        public async Task<IList<WorkoutExerciseSet>> GetExerciseSetsAsync(Guid exerciseId, bool trackChanges) =>
            await FindBy(s => s.WorkoutExerciseId == exerciseId, trackChanges)
                .ToListAsync();

        public async Task<WorkoutExerciseSet?> GetExerciseSetAsync(Guid exerciseId, Guid setId, bool trackChanges) =>
            await FindBy(s => s.WorkoutExerciseId == exerciseId && s.Id == setId, trackChanges)
                .SingleOrDefaultAsync();

        public async Task CreateExerciseSetAsync(Guid exerciseId, WorkoutExerciseSet exerciseSet)
        {
            var currentMaxOrder = await FindBy(s => s.WorkoutExerciseId == exerciseId, false)
                .MaxAsync(s => (int?)s.Order) ?? 0;

            exerciseSet.Order = currentMaxOrder + 1;
            exerciseSet.WorkoutExerciseId = exerciseId;

            Create(exerciseSet);
        }

        public void DeleteExerciseSet(WorkoutExerciseSet exerciseSet)
        {
            var exerciseSets = 
                FindBy(s => s.WorkoutExerciseId == exerciseSet.WorkoutExerciseId, true)
                    .OrderBy(s => s.Order)
                    .ToList();

            var indexToDelete = exerciseSets.FindIndex(s => s.Id == exerciseSet.Id);

            if (indexToDelete < 0) return;

            Delete(exerciseSet);

            for (var i = indexToDelete + 1; i < exerciseSets.Count; i++)
            {
                exerciseSets[i].Order--;
            }
        }
    }
}
