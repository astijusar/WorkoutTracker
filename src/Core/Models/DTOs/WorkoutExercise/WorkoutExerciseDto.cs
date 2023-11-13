using Core.Models.DTOs.Exercise;
using Core.Models.DTOs.WorkoutExerciseSet;

namespace Core.Models.DTOs.WorkoutExercise
{
    public record WorkoutExerciseDto(Guid Id, int Order, ExerciseDto Exercise, ICollection<WorkoutExerciseSetDto> Sets);
}
