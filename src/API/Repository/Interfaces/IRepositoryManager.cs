﻿namespace API.Repository.Interfaces
{
    public interface IRepositoryManager
    {
        void Save();
        Task SaveAsync();
        IExerciseRepository Exercise { get; }
        IWorkoutRepository Workout { get; }
    }
}
