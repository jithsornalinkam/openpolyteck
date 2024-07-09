using System.Net;
using System.Text.Json;

namespace JsonConverterApp.Exceptios
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionHandler(RequestDelegate next)
        {

            _next = next;

        }
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

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;
            ErrorResponse errorMesg = new ErrorResponse();

            switch (exception)
            {
                case ApplicationException ex:
                    errorMesg.ResponseCode = (int)HttpStatusCode.BadRequest;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorMesg.ResponseMessage = "Application Exception Occured, please retry after sometime.";
                    break;
                case System.Xml.XmlException ex:
                    errorMesg.ResponseCode = (int)HttpStatusCode.InternalServerError;
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorMesg.ResponseMessage = ex.Message;
                    break;
                default:
                    errorMesg.ResponseCode = (int)HttpStatusCode.InternalServerError;
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorMesg.ResponseMessage = "Internal Server Error, Please retry after sometime";
                    break;

            }
            var exResult = JsonSerializer.Serialize(errorMesg);
            await context.Response.WriteAsync(exResult);
        }
    }
}
