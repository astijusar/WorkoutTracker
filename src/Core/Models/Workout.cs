using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class Workout
    {
        [Key]
        [Column("WorkoutId")]
        public Guid Id { get; set; }


        [Required(ErrorMessage = "Name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string Name { get; set; } = null!;

        public string? Note { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }


        [Required(ErrorMessage = "IsTemplate is a required field.")]
        public bool IsTemplate { get; set; }


        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public ICollection<WorkoutExercise> Exercises { get; set; } = null!;
    }
}
