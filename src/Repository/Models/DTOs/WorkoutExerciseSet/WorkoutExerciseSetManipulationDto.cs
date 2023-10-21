using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Models.Enums;

namespace Repository.Models.DTOs.WorkoutExerciseSet
{
    public record WorkoutExerciseSetManipulationDto
    {
        [Range(1, 10000, ErrorMessage = "Reps is a required field and it needs to be between 1 and 10000.")]
        public int Reps { get; set; }


        [Column(TypeName = "decimal(18, 2)")]
        [Range(1, 100000, ErrorMessage = "Weight is a required field and it needs to be between 1 and 100000.")]
        public decimal Weight { get; set; }


        [Required(ErrorMessage = "Measurement type is a required field.")]
        public MeasurementType MeasurementType { get; set; }
    }
}
