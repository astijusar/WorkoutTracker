using System.ComponentModel.DataAnnotations;
using Repository.Models.DTOs.WorkoutExerciseSet;

namespace Repository.Models.DTOs.WorkoutExercise
{
    public record WorkoutExerciseUpdateDto
    {
        [Range(1, 1000, ErrorMessage = "Order is a required field and it's value can be between 1 and 1000.")]
        public int Order { get; set; }


        [Required(ErrorMessage = "Sets for exercise are required.")]
        public IEnumerable<WorkoutExerciseSetUpdateDto> Sets { get; set; } = null!;
    }
}
