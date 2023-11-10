using API.Models;
using API.Models.RequestFeatures;

namespace API.Repository.Interfaces
{
    public interface IExerciseRepository
    {
        Task<OffsetPaginationResponse<Exercise>> GetAllExercisesPagedAsync(bool trackChanges, ExerciseParameters param);
        Task<Exercise?> GetExerciseAsync(Guid exerciseId, bool trackChanges);
        void CreateExercise(Exercise exercise);
        void DeleteExercise(Exercise exercise);
    }
}
