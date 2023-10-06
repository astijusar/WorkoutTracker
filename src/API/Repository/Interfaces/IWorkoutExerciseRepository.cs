using API.Models;

namespace API.Repository.Interfaces
{
    public interface IWorkoutExerciseRepository
    {
        Task<IEnumerable<WorkoutExercise>> GetWorkoutExercisesAsync(Guid workoutId, bool trackChanges);
        Task<IEnumerable<WorkoutExercise>> GetWorkoutExercisesAsyncByIds(Guid workoutId, IEnumerable<Guid> exerciseIds, bool trackChanges);
        Task<WorkoutExercise?> GetWorkoutExerciseAsync(Guid workoutId, Guid exerciseId, bool trackChanges);
        Task CreateWorkoutExerciseAsync(Guid workoutId, WorkoutExercise exercise);
        void DeleteWorkoutExercise(WorkoutExercise exercise);
    }
}
