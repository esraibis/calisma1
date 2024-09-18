namespace calisma1.Models
{
    public class Response<T>
    { 
            public T Value { get; set; }
            public bool Success { get; set; }
            public string Message { get; set; }

            public Response(T value, bool success, string message)
            {
                Value = value;
                Success = success;
                Message = message;
            }
        
    }
}

