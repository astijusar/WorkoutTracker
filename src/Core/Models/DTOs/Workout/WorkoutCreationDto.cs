using Core.Models.DTOs.WorkoutExercise;

namespace Core.Models.DTOs.Workout
{
    public record WorkoutCreationDto(string Name, string? Note, DateTime? Start, DateTime? End, bool IsTemplate)
        : WorkoutManipulationDto(Name, Note);
}
