using System;
using Application.Exceptions.Base;
using Microsoft.Extensions.Logging;

namespace Application.Exceptions
{
    public class BadRequestException : ExceptionBase
    {
        public BadRequestException() : base(LogLevel.Debug, "Server cannot or will not process the request due to something that is perceived to be a client error " +
                                                            "(e.g., malformed request syntax, invalid request message framing, or deceptive request routing).")
        {
        }

        public BadRequestException(string message) : base(LogLevel.Debug, message)
        {
        }

        public BadRequestException(string message, Exception innerException) : base(LogLevel.Debug, message, innerException)
        {
        }
    }
}