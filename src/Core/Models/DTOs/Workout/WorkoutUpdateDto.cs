﻿namespace Core.Models.DTOs.Workout
{
    public record WorkoutUpdateDto(string Name, string? Note) : WorkoutManipulationDto(Name, Note);
}
