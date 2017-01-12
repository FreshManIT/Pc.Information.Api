using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Pc.Information.Model.ErrorInfoLog;
using Pc.Information.Utility.Cache;

namespace Pc.Information.Api.MiddleWares
{
    /// <summary>
    /// Error handler middleware
    /// </summary>
    public class ExceptionHandlerMiddleWare
    {
        /// <summary>
        /// next pipe.
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// construct function
        /// </summary>
        /// <param name="next"></param>
        public ExceptionHandlerMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 中间件管道
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// deal error.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception == null) return;
            await WriteExceptionAsync(context, exception).ConfigureAwait(false);
        }

        private static async Task WriteExceptionAsync(HttpContext context, Exception exception)
        {
            //记录日志
            //LogHelper.Error(exception.GetBaseException().ToString());

            var errorModel = new ErrorInfoLogModel { ContentType = context.Request.Headers["Accept"], ErrorMessage = exception?.Message, ErrorTime = DateTime.Now, ErrorTypeFullName = exception?.GetType().FullName, InnerErrorMessage = exception?.InnerException?.Message, StackTrace = exception?.StackTrace };
            RedisCacheHelper.AddListAsync("ErrorList", errorModel, 1);

            //返回友好的提示
            var response = context.Response;

            //状态码
            if (exception is UnauthorizedAccessException)
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
            else if (exception != null)
                response.StatusCode = (int)HttpStatusCode.BadRequest;

            response.ContentType = context.Request.Headers["Accept"];
            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync("<html><head><title>Error</title></head><body>");
            await context.Response.WriteAsync($"<h3 style='color:red;'>{exception?.Message}</h3>");
            await context.Response.WriteAsync($"<p>Type: {exception?.GetType().FullName}");
            await context.Response.WriteAsync($"<p>StackTrace: {exception?.StackTrace}");
            await context.Response.WriteAsync("</body></html>");
        }

    }
}
