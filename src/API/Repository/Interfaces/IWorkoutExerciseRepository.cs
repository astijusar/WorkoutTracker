﻿using API.Models;

namespace API.Repository.Interfaces
{
    public interface IWorkoutExerciseRepository
    {
        Task<IList<WorkoutExercise>> GetWorkoutExercisesAsync(Guid workoutId, bool trackChanges);
        Task<IList<WorkoutExercise>> GetWorkoutExercisesAsyncByIds(Guid workoutId, IList<Guid> exerciseIds, bool trackChanges);
        Task<WorkoutExercise?> GetWorkoutExerciseAsync(Guid workoutId, Guid exerciseId, bool trackChanges);
        Task CreateWorkoutExerciseAsync(Guid workoutId, WorkoutExercise exercise);
        void DeleteWorkoutExercise(WorkoutExercise exercise);
    }
}
