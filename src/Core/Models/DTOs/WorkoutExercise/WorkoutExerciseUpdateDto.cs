using System.ComponentModel.DataAnnotations;
using Core.Models.DTOs.WorkoutExerciseSet;

namespace Core.Models.DTOs.WorkoutExercise
{
    public record WorkoutExerciseUpdateDto
    {
        [Range(1, 1000, ErrorMessage = "Order is a required field and it's value can be between 1 and 1000.")]
        public int Order { get; init; }

        public IEnumerable<WorkoutExerciseSetUpdateDto>? Sets { get; init; }
    }
}
