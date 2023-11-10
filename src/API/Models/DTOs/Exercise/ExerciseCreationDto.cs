namespace API.Models.DTOs.Exercise
{
    public record ExerciseCreationDto(string Name, string? Instructions, string MuscleGroup, string EquipmentType)
        : ExerciseManipulationDto(Name, Instructions, MuscleGroup, EquipmentType);
}
