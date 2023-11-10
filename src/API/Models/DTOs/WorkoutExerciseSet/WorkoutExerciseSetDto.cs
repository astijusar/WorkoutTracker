using API.Models.Enums;

namespace API.Models.DTOs.WorkoutExerciseSet
{
    public record WorkoutExerciseSetDto(Guid Id, int Reps, decimal Weight, MeasurementType MeasurementType, int Order);
}
