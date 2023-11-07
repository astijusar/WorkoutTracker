using API.Repository.Interfaces;

namespace API.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationContext _repositoryContext;
        private IExerciseRepository? _exerciseRepository;
        private IWorkoutRepository? _workoutRepository;
        private IWorkoutExerciseRepository? _workoutExerciseRepository;
        private IWorkoutExerciseSetRepository? _workoutExerciseSetRepository;

        public RepositoryManager(ApplicationContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IExerciseRepository Exercise
        {
            get { return _exerciseRepository ??= new ExerciseRepository(_repositoryContext); }
        }

        public IWorkoutRepository Workout
        {
            get { return _workoutRepository ??= new WorkoutRepository(_repositoryContext); }
        }

        public IWorkoutExerciseRepository WorkoutExercise
        {
            get { return _workoutExerciseRepository ??= new WorkoutExerciseRepository(_repositoryContext); }
        }

        public IWorkoutExerciseSetRepository WorkoutExerciseSet
        {
            get { return _workoutExerciseSetRepository ??= new WorkoutExerciseSetRepository(_repositoryContext); }
        }

        public void Save() => _repositoryContext.SaveChanges();

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
