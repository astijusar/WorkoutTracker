using Core.Models.Enums;

namespace Core.Models.RequestFeatures
{
    public record ExerciseParameters(string? SearchTerm, MuscleGroup? MuscleGroup, Equipment? EquipmentType,
        int PageNumber = 1, int PageSize = 10, bool SortDescending = false);
}
