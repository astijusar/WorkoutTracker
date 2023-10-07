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
        //public ICollection<WorkoutExercise> WorkoutExercises { get; set; } = null!;
    }
}
