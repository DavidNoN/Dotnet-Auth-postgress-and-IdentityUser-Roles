namespace UserApi.Errors
{
    public class ApiErrors
    {
        public ApiErrors(int statusCode, string message, string details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }
        
        private int StatusCode { get; }
        private string Message { get; }
        private string Details { get; }
    }
}