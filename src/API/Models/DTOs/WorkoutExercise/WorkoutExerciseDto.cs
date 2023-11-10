using API.Models.DTOs.Exercise;
using API.Models.DTOs.WorkoutExerciseSet;

namespace API.Models.DTOs.WorkoutExercise
{
    public record WorkoutExerciseDto(Guid Id, int Order, ExerciseDto Exercise, ICollection<WorkoutExerciseSetDto> Sets);
}
