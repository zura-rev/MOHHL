using System;
using HR.Core.Domain.Enums;

namespace HR.Core.Application.Exceptions
{
    public abstract class DataValidationException : Exception
    {
        public abstract int StatusCode { get; }

        public DataValidationException(string message)
            : base(message) { }
    }
}
