using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Models.Enums;

namespace API.Models.DTOs.WorkoutExerciseSet
{
    public record WorkoutExerciseSetManipulationDto
    {
        [Range(1, 10000, ErrorMessage = "Reps is a required field and it needs to be between 1 and 10000.")]
        public int Reps { get; init; }


        [Column(TypeName = "decimal(18, 2)")]
        [Range(1, 100000, ErrorMessage = "Weight is a required field and it needs to be between 1 and 100000.")]
        public decimal Weight { get; init; }

        public MeasurementType MeasurementType { get; init; }
    }
}
