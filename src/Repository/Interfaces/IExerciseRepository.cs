using Repository.Models;

namespace Repository.Interfaces
{
    public interface IExerciseRepository
    {
        Task<IList<Exercise>> GetAllExercisesAsync(bool trackChanges);
        Task<Exercise?> GetExerciseAsync(Guid exerciseId, bool trackChanges);
        void CreateExercise(Exercise exercise);
        void DeleteExercise(Exercise exercise);
    }
}
