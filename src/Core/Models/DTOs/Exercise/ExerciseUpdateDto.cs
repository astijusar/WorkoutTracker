namespace Core.Models.DTOs.Exercise
{
    public record ExerciseUpdateDto(string Name, string? Instructions, string MuscleGroup, string EquipmentType)
        : ExerciseManipulationDto(Name, Instructions, MuscleGroup, EquipmentType);
}
