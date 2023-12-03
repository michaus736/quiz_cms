using Newtonsoft.Json;
using QuizVistaApiBusinnesLayer.Models;
using System.Text;

namespace QuizVistaApi.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await GenerateErrorResponse(context, ex);
            }
        }

        private static async Task GenerateErrorResponse(HttpContext context, Exception? ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 401;
            var errorMessage = new StringBuilder();

            while (ex != null)
            {
                errorMessage.Append(ex.Message + "\n");
                ex = ex.InnerException;
            }

            await context.Response.WriteAsync(JsonConvert.SerializeObject(Result.Failed(errorMessage.ToString())));
        }
    }
}
