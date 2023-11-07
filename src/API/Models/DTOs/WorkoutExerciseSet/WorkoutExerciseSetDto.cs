using API.Models.Enums;

namespace API.Models.DTOs.WorkoutExerciseSet
{
    public record WorkoutExerciseSetDto
    {
        public Guid Id { get; set; }
        public int Reps { get; set; }
        public decimal Weight { get; set; }
        public MeasurementType MeasurementType { get; set; }
        public int Order { get; set; }
    }
}
