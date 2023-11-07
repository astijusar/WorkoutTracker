using Repository.Models.Enums;

namespace Repository.Models.DTOs.Exercise
{
    // TODO: refactor all records to have init
    public record ExerciseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Instructions { get; set; } = null!;
        public MuscleGroup MuscleGroup { get; set; }
        public Equipment EquipmentType { get; set; }
    }
}
