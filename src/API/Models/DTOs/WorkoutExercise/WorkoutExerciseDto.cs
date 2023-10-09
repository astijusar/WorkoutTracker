using API.Models.DTOs.Exercise;
using API.Models.DTOs.WorkoutExerciseSet;

namespace API.Models.DTOs.WorkoutExercise
{
    public record WorkoutExerciseDto
    {
        public Guid Id { get; set; }
        public int Order { get; set; }
        public ExerciseDto Exercise { get; set; } = null!;
        public ICollection<WorkoutExerciseSetDto> Sets { get; set; } = null!;
    }
}
