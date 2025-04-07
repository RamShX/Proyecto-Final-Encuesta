namespace WebApi.Response
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; } = default!;
        public ApiResponse() { }

    }
}
