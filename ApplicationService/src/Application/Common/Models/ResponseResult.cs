using System.Text.Json.Serialization;

namespace Application.Common.Models
{
    public class ResponseResult<T>
    {
        /// <summary>
        /// Indicates whether the operation was successful or not.
        /// </summary>
        [JsonPropertyName("status")]
        public bool Status { get; set; } = true;

        /// <summary>
        /// Optional message, usually used in failure or confirmation.
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; set; }

        /// <summary>
        /// Contains the data returned from the API if the operation was successful.
        /// </summary>
        [JsonPropertyName("data")]
        public ApiResponse<T>? Data { get; set; }

        /// <summary>
        /// Contains the exception (if any) encountered during the operation.
        /// Not serialized unless explicitly configured.
        /// </summary>
        [JsonIgnore] // Prevent exposing internal exception details in production
        public Exception? Exception { get; set; }

        public ResponseResult() { }

        public ResponseResult(ApiResponse<T>? data = default)
        {
            Data = data;
        }
    }

    public static class ResponseResult
    {
        public static ResponseResult<T> Success<T>(ApiResponse<T> data) =>
            new ResponseResult<T>(data);

        public static ResponseResult<T> Failure<T>(string message, Exception? exception = null)
        {
            return new ResponseResult<T>
            {
                Status = false,
                Message = message,
                Data = null,
                Exception = exception
            };
        }
    }
}
