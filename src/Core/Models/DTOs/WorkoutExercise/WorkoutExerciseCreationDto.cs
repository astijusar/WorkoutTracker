using Core.Models.DTOs.WorkoutExerciseSet;

namespace Core.Models.DTOs.WorkoutExercise
{
    public record WorkoutExerciseCreationDto(Guid ExerciseId, ICollection<WorkoutExerciseSetCreationDto> Sets);
}
