using API.Models.DTOs.WorkoutExercise;

namespace API.Models.DTOs.Workout
{
    public record WorkoutDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Note { get; set; } = null!;
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public bool IsTemplate { get; set; }
        public ICollection<WorkoutExerciseDto> WorkoutExercises { get; set; } = null!;
    }
}
