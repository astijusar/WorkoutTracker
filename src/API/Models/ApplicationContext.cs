﻿using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Exercise>()
                .HasMany(e => e.WorkoutExercises)
                .WithOne(w => w.Exercise)
                .HasForeignKey(w => w.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Workout>()
                .HasMany(w => w.WorkoutExercises)
                .WithOne(e => e.Workout)
                .HasForeignKey(e => e.WorkoutId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            modelBuilder.Entity<WorkoutExercise>()
                .HasMany(w => w.WorkoutExerciseSets)
                .WithOne(we => we.WorkoutExercise)
                .HasForeignKey(we => we.WorkoutExerciseId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercises { get; set; }
        public DbSet<WorkoutExerciseSet> WorkoutExerciseSets { get; set; }
    }
}
