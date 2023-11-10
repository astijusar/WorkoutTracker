using System.ComponentModel.DataAnnotations;
using API.Models.DTOs.WorkoutExerciseSet;

namespace API.Models.DTOs.WorkoutExercise
{
    public record WorkoutExerciseCreationDto(Guid ExerciseId, ICollection<WorkoutExerciseSetCreationDto> Sets);
}
