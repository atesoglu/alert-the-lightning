using System.Collections.Generic;
using System.Linq;
using Application.Exceptions.Base;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace Application.Exceptions
{
    public class ValidationException : ExceptionBase
    {
        public ICollection<KeyValuePair<string, ICollection<string>>> Errors { get; }

        public ValidationException() : base(LogLevel.Debug)
        {
        }

        public ValidationException(IEnumerable<ValidationFailure> errors) : base(LogLevel.Debug, "One or more validation errors have occurred.")
        {
            Errors = errors
                .GroupBy(g => g.PropertyName, g => g.ErrorMessage)
                .Select(s => new KeyValuePair<string, ICollection<string>>(s.Key, s.ToList())).ToList();
        }
    }
}