namespace Repository.Models.DTOs.Workout
{
    public record WorkoutManipulationDto
    {
        public string Name { get; set; } = null!;
        public string? Note { get; set; }
    }
}
