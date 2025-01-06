namespace Database.Exceptions
{
    public class RepositoryOperationExceptions : Exception
    {
        public RepositoryOperationExceptions() : base() { }
        public RepositoryOperationExceptions(string message) : base(message) { }
        public RepositoryOperationExceptions(string message, Exception exception) : base(message, exception) { }
    }
}