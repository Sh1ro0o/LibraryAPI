using LibraryAPI.Common.Enums;

namespace LibraryAPI.Common
{
    public class OperationResult<T>
    {
        public bool IsSuccessful { get; set; }
        public string? Message { get; set; }
        public OperationErrorType? ErrorType { get; set; }
        public T? Data { get; set; }


        public static OperationResult<T> Success(T data)
        {
            return new OperationResult<T>
            {
                IsSuccessful = true,
                Data = data
            };
        }

        public static OperationResult<T> Success()
        {
            return new OperationResult<T>
            {
                IsSuccessful = true
            };
        }

        public static OperationResult<T> NotFound(T? data = default, string? message = null)
        {
            return new OperationResult<T>
            {
                Data = data,
                IsSuccessful = false,
                Message = message,
                ErrorType = OperationErrorType.NotFound
            };
        }

        public static OperationResult<T> Conflict(T? data = default, string? message = null)
        {
            return new OperationResult<T>
            {
                Data = data,
                IsSuccessful = false,
                Message = message,
                ErrorType = OperationErrorType.Conflict
            };
        }

        public static OperationResult<T> BadRequest(T? data = default, string? message = null)
        {
            return new OperationResult<T>
            {
                Data = data,
                IsSuccessful = false,
                Message = message,
                ErrorType = OperationErrorType.BadRequest
            };
        }

        public static OperationResult<T> InternalServerError(T? data = default, string? message = null)
        {
            return new OperationResult<T>
            {
                Data = data,
                IsSuccessful = false,
                Message = message,
                ErrorType = OperationErrorType.InternalServerError
            };
        }
    }
}
