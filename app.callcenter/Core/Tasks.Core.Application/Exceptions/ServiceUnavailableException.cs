using System;
using System.Net;
using Tasks.Core.Domain.Enums;

namespace Tasks.Core.Application.Exceptions
{
    public class ServiceUnavailableException : DataValidationException
    {
        public override int StatusCode => (int)HttpStatusCode.ServiceUnavailable;

        public ServiceUnavailableException(string message)
            : base(message) { }
    }
}
