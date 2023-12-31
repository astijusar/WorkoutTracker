﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Enums;

namespace Core.Models
{
    [Table("workout_exercise_set")]
    public class WorkoutExerciseSet
    {
        [Key]
        [Column("WorkoutExerciseSetId")]
        public Guid Id { get; set; }


        [Range(1, 10000, ErrorMessage = "Reps is a required field and it needs to be between 1 and 10000.")]
        public int Reps { get; set; }


        [Column(TypeName = "decimal(18, 2)")]
        [Range(0.01, 100000, ErrorMessage = "Weight is a required field and it needs to be between 0.01 and 100000.")]
        public decimal Weight { get; set; }


        [Required(ErrorMessage = "Measurement type is a required field.")]
        public MeasurementType MeasurementType { get; set; }


        [Required(ErrorMessage = "Order is a required field.")]
        public int Order { get; set; }

        [Required(ErrorMessage = "Done is a required field.")]
        public bool Done { get; set; } = false;


        public Guid WorkoutExerciseId { get; set; }
        public WorkoutExercise WorkoutExercise { get; set; } = null!;
    }
}
