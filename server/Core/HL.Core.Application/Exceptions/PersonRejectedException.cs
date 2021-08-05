using System;
using System.Net;
using HL.Core.Domain.Enums;

namespace HL.Core.Application.Exceptions
{
    public class PersonRejectedException : DataValidationException
    {
        public override int StatusCode => (int)HttpStatusCode.NotAcceptable;

        public PersonRejectedException(string message)
            : base(message) { }
    }
}
