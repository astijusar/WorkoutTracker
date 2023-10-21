using System.ComponentModel.DataAnnotations;

namespace Repository.Models.DTOs.Workout
{
    public record WorkoutCreationDto : WorkoutManipulationDto
    {
        [Required(ErrorMessage = "Start is a required field.")]
        public DateTime Start { get; set; }

        [Required(ErrorMessage = "End is a required field.")]
        public DateTime End { get; set; }

        public bool IsTemplate { get; set; }
    }
}
