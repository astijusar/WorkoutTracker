using API.Models;
using API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class ExerciseRepository : RepositoryBase<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(ApplicationContext repositoryContext) 
            : base(repositoryContext)
        {
        }

        public async Task<IList<Exercise>> GetAllExercisesAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .ToListAsync();

        public async Task<Exercise?> GetExerciseAsync(Guid exerciseId, bool trackChanges) =>
            await FindBy(e => e.Id == exerciseId, trackChanges)
                .SingleOrDefaultAsync();

        public void CreateExercise(Exercise exercise) => Create(exercise);

        public void DeleteExercise(Exercise exercise) => Delete(exercise);
    }
}
