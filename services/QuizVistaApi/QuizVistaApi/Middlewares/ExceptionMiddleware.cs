using Newtonsoft.Json;
using QuizVistaApiBusinnesLayer.Models;

namespace QuizVistaApi.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await GenerateErrorResponse(context, ex);
            }
        }

        private static async Task GenerateErrorResponse(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(Result.Failed(ex.Message)));
        }
    }
}
