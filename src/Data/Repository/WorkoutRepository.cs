using Core.Models;
using Core.Models.RequestFeatures;
using Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class WorkoutRepository : RepositoryBase<Workout>, IWorkoutRepository
    {
        public WorkoutRepository(ApplicationContext repositoryContext) 
            : base(repositoryContext)
        {
        }

        public async Task<OffsetPaginationResponse<Workout>> GetAllWorkoutsAsync(string userId, bool trackChanges, WorkoutParameters param)
        {
            var searchTerm = param.SearchTerm?.Trim().ToLower();

            var query = searchTerm != null
                ? FindBy(w => w.UserId == userId && w.Name == searchTerm, trackChanges)
                : FindBy(w => w.UserId == userId, trackChanges);

            query = query.Filter(param.Template, w => w.IsTemplate == param.Template);
            query = query.Filter(param.StartFrom, w => w.Start >= param.StartFrom);
            query = query.Filter(param.EndTo, w => w.End >= param.EndTo);

            var workoutsCount = query.Count();

            var workouts = await query
                .Sort(e => e.Name, param.SortDescending)
                .Skip((param.PageNumber - 1) * param.PageSize)
                .Take(param.PageSize)
                .ToListAsync();

            var metadata = new OffsetPaginationMetadata(workoutsCount,
                param.PageNumber, param.PageSize);

            return new OffsetPaginationResponse<Workout>(workouts, metadata);
        }

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
