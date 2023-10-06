using API.Models;

namespace API.Repository.Interfaces
{
    public interface IWorkoutRepository
    {
        Task<IEnumerable<Workout>> GetAllWorkoutsAsync(bool trackChanges);
        Task<Workout?> GetWorkoutAsync(Guid workoutId, bool trackChanges);
        void CreateWorkout(Workout workout);
        void DeleteWorkout(Workout workout);
    }
}
