using API.Models;

namespace API.Repository.Interfaces
{
    public interface IExerciseRepository
    {
        Task<IEnumerable<Exercise>> GetAllExercisesAsync(bool trackChanges);
        Task<Exercise?> GetExerciseAsync(Guid exerciseId, bool trackChanges);
        void CreateExercise(Exercise exercise);
        void DeleteExercise(Exercise exercise);
    }
}
