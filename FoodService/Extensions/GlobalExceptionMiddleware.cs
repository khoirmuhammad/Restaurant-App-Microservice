using FoodService.Models.Customs;
using System.Net;

namespace FoodService.Extensions
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalExceptionMiddleware> logger;
        private readonly IWebHostEnvironment env;

        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger,
            IWebHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                ResponseModel<object> response;

                int statusCode = GetStatusCodeFromException(ex);

                if (env.IsDevelopment())
                {
                    response = new ResponseModel<object>(ex.Message, ex.StackTrace, null);
                }
                else
                {
                    response = new ResponseModel<object>(ex.Message, null, null);
                }

                logger.LogError(ex, ex.Message);
                httpContext.Response.StatusCode = statusCode;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(response.ToString());
            }
        }

        private int GetStatusCodeFromException(Exception ex)
        {
            var exceptionType = ex.GetType();

            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                return (int)HttpStatusCode.Unauthorized;
            }
            else
            {
                return (int)HttpStatusCode.InternalServerError;
            }
        }

    }
}
