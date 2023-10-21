using Repository.Models;

namespace Repository.Interfaces
{
    public interface IWorkoutRepository
    {
        Task<IList<Workout>> GetAllWorkoutsAsync(bool trackChanges);
        Task<Workout?> GetWorkoutAsync(Guid workoutId, bool trackChanges);
        void CreateWorkout(Workout workout);
        void DeleteWorkout(Workout workout);
    }
}
