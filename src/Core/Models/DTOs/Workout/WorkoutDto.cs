using Core.Models.DTOs.WorkoutExercise;

namespace Core.Models.DTOs.Workout
{
    public record WorkoutDto(Guid Id, string Name, string Note, DateTime? Start, DateTime? End, bool IsTemplate,
        ICollection<WorkoutExerciseDto>? WorkoutExercises);
}
