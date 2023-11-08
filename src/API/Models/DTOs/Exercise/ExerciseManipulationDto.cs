﻿using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.Exercise
{
    public record ExerciseManipulationDto
    {
        [Required(ErrorMessage = "Name is a required field.")]
        public string Name { get; set; } = null!;

        public string? Instructions { get; set; }


        [Required(ErrorMessage = "Muscle group is a required field.")]
        public string MuscleGroup { get; set; } = null!;


        [Required(ErrorMessage = "Equipment type is a required field.")]
        public string EquipmentType { get; set; } = null!;
    }
}