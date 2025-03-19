namespace LibraryAPI.Common
{
    public class OperationResult
    {
        public bool IsSuccessful { get; set; }
        public string? Message { get; set; }
        public OperationErrorType? ErrorType { get; set; }

        public static OperationResult Success()
        {
            return new OperationResult { 
                IsSuccessful = true 
            };
        }

        public static OperationResult NotFound(string? message = null)
        {
            return new OperationResult
            {
                IsSuccessful = false,
                Message = message,
                ErrorType = OperationErrorType.NotFound
            };
        }

        public static OperationResult Conflict(string? message = null)
        {
            return new OperationResult
            {
                IsSuccessful = false,
                Message = message,
                ErrorType = OperationErrorType.Conflict
            };
        }
    }

    public class OperationResult<T> : OperationResult
    {
        public T? Data { get; set; }

        public static OperationResult<T> Success(T data)
        {
            return new OperationResult<T>
            {
                IsSuccessful = true,
                Data = data
            };
        }
    }

    public enum OperationErrorType
    {
        NotFound = 0,
        Conflict = 1,
        Validation = 2,
    }
}
