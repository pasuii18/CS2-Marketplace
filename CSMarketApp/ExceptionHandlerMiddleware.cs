using Newtonsoft.Json;
using System.Data;
using System.Net;
using System.Security.Authentication;

namespace CSMarketApp
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ArgumentNullException ex){ await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError);  }
            catch (KeyNotFoundException ex){ await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound);  }
            catch (DuplicateNameException ex) { await HandleExceptionAsync(context, ex, HttpStatusCode.Conflict);  }
            catch (AuthenticationException ex) { await HandleExceptionAsync(context, ex, HttpStatusCode.Unauthorized);  }
            catch (FileLoadException ex) { await HandleExceptionAsync(context, ex, HttpStatusCode.UnsupportedMediaType); }
            catch (InvalidDataException ex) { await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest); }
            catch (BadHttpRequestException ex) { await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest); }
            catch (Exception ex){ await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError); }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var errorDetails = new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            };

            var errorJson = JsonConvert.SerializeObject(errorDetails);
            return context.Response.WriteAsync(errorJson);
        }
    }
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
