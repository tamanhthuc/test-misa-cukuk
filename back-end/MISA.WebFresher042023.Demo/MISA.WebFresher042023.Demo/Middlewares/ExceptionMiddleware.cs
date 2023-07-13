using MISA.WebFresher042023.Demo.Core.MISAException;
using MISA.WebFresher042023.Demo.MISAException;
  
namespace MISA.WebFresher042023.Demo.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
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
            context.Response.ContentType = "application/json; charset=utf-8";

            if (exception is NotFoundException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = ((NotFoundException)exception).ErrorCode,
                        UserMessage = "Không tìm thấy tài nguyên thông cảm!",
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? ""
                    );
            }
            else if (exception is DuplicatedCodeException duplicatedCodeException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = context.Response.StatusCode,
                        UserMessage = "",
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink,
                        Errors = duplicatedCodeException.Error
                    }.ToString() ?? ""
                    );
            }
            else if (exception is ValidateException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = context.Response.StatusCode,
                        UserMessage = "Lỗi từ người dùng",
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? "");


            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsync(text: new BaseException()
                {
                    ErrorCode = context.Response.StatusCode,
                    UserMessage = "Lỗi hệ thống",
                    DevMessage = exception.Message,
                    TraceId = context.TraceIdentifier,  
                    MoreInfo = exception.HelpLink
                }.ToString() ?? "" );
            }

        }
    }
}
