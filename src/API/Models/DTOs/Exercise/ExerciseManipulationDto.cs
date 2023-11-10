using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.Exercise
{
    public record ExerciseManipulationDto(string Name, string? Instructions, string MuscleGroup, string EquipmentType);
}
