using System;
using System.Net;
using Tasks.Core.Domain.Enums;

namespace Tasks.Core.Application.Exceptions
{
    public class BadRequestException : DataValidationException
    {
        public override int StatusCode => (int)HttpStatusCode.BadRequest;

        public BadRequestException(string message)
            : base(message) { }
    }
}
