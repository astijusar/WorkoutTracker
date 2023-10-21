using Repository.Models;

namespace Repository.Interfaces
{
    public interface IWorkoutExerciseSetRepository
    {
        Task<IList<WorkoutExerciseSet>> GetExerciseSetsAsync(Guid exerciseId, bool trackChanges);
        Task<WorkoutExerciseSet?> GetExerciseSetAsync(Guid exerciseId, Guid setId, bool trackChanges);
        Task CreateExerciseSetAsync(Guid exerciseId, WorkoutExerciseSet exerciseSet);
        void DeleteExerciseSet(WorkoutExerciseSet exerciseSet);
    }
}
