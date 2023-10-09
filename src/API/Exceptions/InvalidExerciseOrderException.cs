namespace API.Exceptions
{
    public class InvalidExerciseOrderException : Exception
    {
        public InvalidExerciseOrderException() 
            : base("Invalid exercise order.")
        {
            
        }
    }
}
