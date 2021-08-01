using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Exceptions.Base;
using Application.Response;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middlewares
{
    // ReSharper disable once ClassNeverInstantiated.Global
    // Can not be abstract since it's registered via DI
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
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

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var response = new ErrorResponseModel();
            response.AddParam("path", context.Request.GetDisplayUrl());
            response.AddParam("trace-identifier", context.TraceIdentifier);

            var logLevel = ex is ExceptionBase eb ? eb.LogLevel : LogLevel.Error;
            _logger.Log(logLevel, ex, "CorrelationId: {CorrelationId} TraceIdentifier: {TraceIdentifier} Path: {Path}", response.CorrelationId, context.TraceIdentifier, context.Request.GetDisplayUrl());

            if (_environment.IsDevelopment())
            {
                response.AddError("_message", ex.Message);
                response.AddError("_stack-trace", ex.StackTrace);
                response.AddError("_inner-exception", ex.InnerException?.ToString());
            }

            var httpStatusCode = HttpStatusCode.InternalServerError;
            response.Message = "The server encountered an internal error or misconfiguration and was unable to complete your request.";

            if (ex is ExceptionBase exceptionBase)
            {
                response.Message = exceptionBase.Message;

                switch (ex)
                {
                    case BadRequestException badRequestException:
                        httpStatusCode = HttpStatusCode.BadRequest;
                        break;

                    case ValidationException validationException:
                    {
                        httpStatusCode = HttpStatusCode.BadRequest;
                        if (validationException.Errors != null)
                            foreach (var error in validationException.Errors)
                                response.AddError(error.Key, string.Join(" ", error.Value));

                        break;
                    }
                }
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) httpStatusCode;

            await context.Response.WriteAsync(JsonSerializer.Serialize(response, new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase}));
        }
    }
}