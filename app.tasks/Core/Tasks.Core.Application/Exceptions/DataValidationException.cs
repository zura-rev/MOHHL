using System;
using Tasks.Core.Domain.Enums;

namespace Tasks.Core.Application.Exceptions
{
    public abstract class DataValidationException : Exception
    {
        public abstract int StatusCode { get; }

        public DataValidationException(string message)
            : base(message) { }
    }
}
