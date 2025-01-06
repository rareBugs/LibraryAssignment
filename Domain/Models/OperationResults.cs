namespace Domain.Models
{
    public class OperationResults<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public T Data { get; set; }
        public string ErrorMessage { get; set; }

        public OperationResults(bool success, string message, T data, string errorMessage)
        {
            IsSuccess = success;
            Message = message;
            Data = data;
            ErrorMessage = errorMessage;
        }

        public static OperationResults<T> SuccessResult(T data, string message = "Operation successful")
        {
            return new OperationResults<T>(true, message, data, null);
        }

        public static OperationResults<T> FailureResult(string errorMessage, string message = "Operation failed")
        {
            return new OperationResults<T>(false, message, default, errorMessage);
        }
    }
}