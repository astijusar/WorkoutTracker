using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Workout
    {
        [Key]
        [Column("WorkoutId")]
        public Guid Id { get; set; }


        [Required(ErrorMessage = "Name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string Name { get; set; } = null!;


        [Required(ErrorMessage = "Note is a required field.")]
        [MaxLength(200, ErrorMessage = "Maximum length for the Note is 200 characters.")]
        public string Note { get; set; } = null!;

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public bool IsTemplate { get; set; }


        public ICollection<WorkoutExercise> WorkoutExercises { get; set; } = null!;
    }
}
