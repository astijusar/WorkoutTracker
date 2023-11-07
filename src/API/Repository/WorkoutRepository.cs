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

        public async Task<IList<Workout>> GetAllWorkoutsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .ToListAsync();

        public async Task<Workout?> GetWorkoutAsync(Guid workoutId, bool trackChanges) =>
            await FindBy(w => w.Id == workoutId, trackChanges)
                .Include(w => w.Exercises)
                    .ThenInclude(we => we.Exercise)
                .Include(w => w.Exercises)
                    .ThenInclude(we => we.Sets)
                .SingleOrDefaultAsync();

        public void CreateWorkout(Workout workout) => Create(workout);

        public void DeleteWorkout(Workout workout) => Delete(workout);
    }
}
