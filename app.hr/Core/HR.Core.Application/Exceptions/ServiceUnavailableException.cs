using System;
using System.Net;
using HR.Core.Domain.Enums;

namespace HR.Core.Application.Exceptions
{
    public class ServiceUnavailableException : DataValidationException
    {
        public override int StatusCode => (int)HttpStatusCode.ServiceUnavailable;

        public ServiceUnavailableException(string message)
            : base(message) { }
    }
}
