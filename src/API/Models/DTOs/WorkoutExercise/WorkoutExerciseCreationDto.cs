using System.ComponentModel.DataAnnotations;
using API.Models.DTOs.WorkoutExerciseSet;

namespace API.Models.DTOs.WorkoutExercise
{
    public record WorkoutExerciseCreationDto
    {
        [Required(ErrorMessage = "ExerciseId is a required field.")]
        public Guid ExerciseId { get; set; }


        [Required(ErrorMessage = "Sets for exercise are required.")]
        public IEnumerable<WorkoutExerciseSetCreationDto> Sets { get; set; } = null!;
    }
}
