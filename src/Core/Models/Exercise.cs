using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Enums;

namespace Core.Models
{
    public class Exercise
    {
        [Key]
        [Column("ExerciseId")]
        public Guid Id { get; set; }


        [Required(ErrorMessage = "Name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Name is 50 characters.")]
        public string Name { get; set; } = null!;


        public string? Instructions { get; set; }


        [Required(ErrorMessage = "Muscle group is a required field.")]
        public MuscleGroup MuscleGroup { get; set; }


        [Required(ErrorMessage = "Equipment type is a required field.")]
        public Equipment EquipmentType { get; set; }


        public ICollection<WorkoutExercise>? WorkoutExercises { get; set; }
    }
}
