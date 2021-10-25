using System;
using System.Net;
using HR.Core.Domain.Enums;

namespace HR.Core.Application.Exceptions
{
    public class PersonRejectedException : DataValidationException
    {
        public override int StatusCode => (int)HttpStatusCode.NotAcceptable;

        public PersonRejectedException(string message)
            : base(message) { }
    }
}
