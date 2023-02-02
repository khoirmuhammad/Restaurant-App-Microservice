using System.Text.Json;

namespace UserService.Models.Customs
{
    public class ResponseModel<T>
    {
        public ResponseModel()
        {

        }

        public ResponseModel(string errorMessage, string? stackTrace, T? data)
        {
            ErrorMessage = errorMessage;
            StackTrace = stackTrace;
            Data = data;
        }
        public string ErrorMessage { get; set; } = string.Empty;
        public string? StackTrace { get; set; }
        public T? Data { get; set; } = default!;

        public override string ToString()
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return JsonSerializer.Serialize(this, options);
        }
    }
}
