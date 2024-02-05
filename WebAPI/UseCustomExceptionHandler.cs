using Business.Exceptions;
using Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;
using System.Xml.Linq;

namespace WebAPI
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var statusCode = StatusCodes.Status500InternalServerError;
                    switch (exceptionFeature.Error)
                    {
                        case BadRequestException:
                            statusCode = StatusCodes.Status400BadRequest;
                            break;
                        case UnauthorizedAccessException:
                            statusCode = StatusCodes.Status401Unauthorized;
                            break;
                        case NotFoundException:
                            statusCode = StatusCodes.Status404NotFound;
                            break;
                    }

                    context.Response.StatusCode = statusCode;
                    var response = ApiResponse<NoData>.Fail(statusCode, exceptionFeature.Error.Message);
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });
            });
        }
    }
}
