namespace Application.Common.Models
{
    public class ApiResponse<T>
    {
        /// <summary>
        /// dữ liệu nhận được từ service khác
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// thông báo từ service 
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        /// mã trạng thái có thể thành công hoặc thất bại
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// mã api
        /// </summary>
        public string? Code { get; set; }
    }
}
