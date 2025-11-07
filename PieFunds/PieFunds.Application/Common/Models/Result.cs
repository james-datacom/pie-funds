namespace PieFunds.Application.Common.Models
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public string? ErrorCode { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static Result<T> Ok(T data, string? message = null) => new Result<T> { Success = true, Data = data };
        public static Result<T> Fail(string? errorCode = null, string? message = null) => new Result<T> { Success = false, ErrorCode = errorCode, Message = message };

    }
}
