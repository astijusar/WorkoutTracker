using API.Models;
using API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class WorkoutRepository : RepositoryBase<Workout>, IWorkoutRepository
    {
        public WorkoutRepository(ApplicationContext repositoryContext) 
            : base(repositoryContext)
        {
        }

        public async Task<IList<Workout>> GetAllWorkoutsAsync(string userId, bool trackChanges) =>
            await FindBy(w => w.UserId == userId, trackChanges)
                .ToListAsync();

        public async Task<Workout?> GetWorkoutAsync(string userId, Guid workoutId, bool trackChanges) =>
            await FindBy(w => w.UserId == userId && w.Id == workoutId, trackChanges)
                .Include(w => w.Exercises)
                    .ThenInclude(we => we.Exercise)
                .Include(w => w.Exercises)
                    .ThenInclude(we => we.Sets)
                .SingleOrDefaultAsync();

        public void CreateWorkout(string userId, Workout workout)
        {
            workout.UserId = userId;
            Create(workout);
        }

        public void DeleteWorkout(Workout workout) => Delete(workout);
    }
}
