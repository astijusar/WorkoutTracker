using Core.Models;
using Core.Models.RequestFeatures;

namespace Data.Repository.Interfaces
{
    public interface IExerciseRepository
    {
        Task<OffsetPaginationResponse<Exercise>> GetAllExercisesPagedAsync(bool trackChanges, ExerciseParameters param);
        Task<Exercise?> GetExerciseAsync(Guid exerciseId, bool trackChanges);
        void CreateExercise(Exercise exercise);
        void DeleteExercise(Exercise exercise);
    }
}
