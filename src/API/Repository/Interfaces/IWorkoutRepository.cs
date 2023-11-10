using API.Models;

namespace API.Repository.Interfaces
{
    public interface IWorkoutRepository
    {
        Task<IList<Workout>> GetAllWorkoutsAsync(string userId, bool trackChanges);
        Task<Workout?> GetWorkoutAsync(string userId, Guid workoutId, bool trackChanges);
        void CreateWorkout(string userId, Workout workout);
        void DeleteWorkout(Workout workout);
    }
}
