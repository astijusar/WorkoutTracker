using Core.Models.Enums;

namespace Core.Models.DTOs.WorkoutExerciseSet
{
    public record WorkoutExerciseSetDto(Guid Id, int Reps, decimal Weight, MeasurementType MeasurementType, int Order, bool Done);
}
