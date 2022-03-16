using System;
using System.Net;
using Tasks.Core.Domain.Enums;

namespace Tasks.Core.Application.Exceptions
{
    public class PersonRejectedException : DataValidationException
    {
        public override int StatusCode => (int)HttpStatusCode.NotAcceptable;

        public PersonRejectedException(string message)
            : base(message) { }
    }
}
