using API.Models;
using API.Models.RequestFeatures;

namespace API.Repository.Interfaces
{
    public interface IWorkoutRepository
    {
        Task<OffsetPaginationResponse<Workout>> GetAllWorkoutsAsync(string userId, bool trackChanges, WorkoutParameters param);
        Task<Workout?> GetWorkoutAsync(string userId, Guid workoutId, bool trackChanges);
        void CreateWorkout(string userId, Workout workout);
        void DeleteWorkout(Workout workout);
    }
}
