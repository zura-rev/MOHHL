using System;
using System.Net;
using Tasks.Core.Domain.Enums;

namespace Tasks.Core.Application.Exceptions
{
    public class DataNotFoundException : DataValidationException
    {
        public override int StatusCode => (int)HttpStatusCode.NotFound;

        public DataNotFoundException(string message)
            : base(message) { }
    }
}
