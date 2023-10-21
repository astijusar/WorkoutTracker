using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models
{
    [Table("workout_exercise")]
    public class WorkoutExercise
    {
        [Key]
        [Column("WorkoutExerciseId")]
        public Guid Id { get; set; }


        [Range(1, 1000, ErrorMessage = "Order is a required field and it's value can be between 1 and 1000.")]
        public int Order { get; set; }


        public Guid WorkoutId { get; set; }
        public Workout Workout { get; set; } = null!;


        public Guid ExerciseId { get; set; }
        public Exercise Exercise { get; set; } = null!;


        public ICollection<WorkoutExerciseSet> Sets { get; set; } = null!;
    }
}
