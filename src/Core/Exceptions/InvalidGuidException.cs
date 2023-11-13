namespace Core.Exceptions
{
    public class InvalidGuidException : Exception
    {
        public InvalidGuidException(string name)
            : base($"'{name}' is not a valid Guid.")

        {
        }
    }
}
