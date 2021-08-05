using System;
using System.Net;
using HL.Core.Domain.Enums;

namespace HL.Core.Application.Exceptions
{
    public class ServiceUnavailableException : DataValidationException
    {
        public override int StatusCode => (int)HttpStatusCode.ServiceUnavailable;

        public ServiceUnavailableException(string message)
            : base(message) { }
    }
}
