namespace WebApi.Response
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; } = default!;

        //Method constructor and overload
        public ApiResponse() { }

        public ApiResponse(T data, string mensaje)
        {
            Data = data;
            Message = mensaje;
        }

        public ApiResponse(string mensaje)
        {
            Success = false;
            Message = mensaje;
        }

    }
}
