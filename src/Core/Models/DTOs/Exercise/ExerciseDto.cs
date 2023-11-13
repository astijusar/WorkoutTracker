using Core.Models.Enums;

namespace Core.Models.DTOs.Exercise
{
    public record ExerciseDto(Guid Id, string Name, string Instructions, MuscleGroup MuscleGroup,
        Equipment EquipmentType);
}
