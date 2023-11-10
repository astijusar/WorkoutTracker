﻿using API.Models;
using API.Models.RequestFeatures;
using API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class ExerciseRepository : RepositoryBase<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(ApplicationContext repositoryContext) 
            : base(repositoryContext)
        {
        }

        public async Task<OffsetPaginationResponse<Exercise>> GetAllExercisesPagedAsync(bool trackChanges, ExerciseParameters param)
        {
            var searchTerm = param.SearchTerm?.Trim().ToLower();

            var query = searchTerm != null
                ? FindBy(e => e.Name.ToLower().Contains(searchTerm), trackChanges)
                : FindAll(trackChanges);

            query = query.Filter(param.MuscleGroup, e => e.MuscleGroup == param.MuscleGroup);
            query = query.Filter(param.EquipmentType, e => e.EquipmentType == param.EquipmentType);

            if (param.EquipmentType != null)
            {
                query = query.Where(e => e.EquipmentType == param.EquipmentType);
            }

            var exercises = await query
                .Sort(e => e.Name, param.SortDescending)
                .Skip((param.PageNumber - 1) * param.PageSize)
                .Take(param.PageSize)
                .ToListAsync();

            var metadata = new OffsetPaginationMetadata(RepositoryContext.Exercises.Count(),
                param.PageNumber, param.PageSize);

            return new OffsetPaginationResponse<Exercise>(exercises, metadata);
        }

        public async Task<Exercise?> GetExerciseAsync(Guid exerciseId, bool trackChanges) =>
            await FindBy(e => e.Id == exerciseId, trackChanges)
                .SingleOrDefaultAsync();

        public void CreateExercise(Exercise exercise) => Create(exercise);

        public void DeleteExercise(Exercise exercise) => Delete(exercise);
    }
}
