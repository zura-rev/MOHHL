using System;
using System.Net;
using HR.Core.Domain.Enums;

namespace HR.Core.Application.Exceptions
{
    public class DataNotFoundException : DataValidationException
    {
        public override int StatusCode => (int)HttpStatusCode.NotFound;

        public DataNotFoundException(string message)
            : base(message) { }
    }
}
