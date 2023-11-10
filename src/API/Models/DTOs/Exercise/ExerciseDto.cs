using API.Models.Enums;

namespace API.Models.DTOs.Exercise
{
    public record ExerciseDto(Guid Id, string Name, string Instructions, MuscleGroup MuscleGroup,
        Equipment EquipmentType);
}
