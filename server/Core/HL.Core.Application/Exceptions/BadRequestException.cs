using System;
using System.Net;
using HL.Core.Domain.Enums;

namespace HL.Core.Application.Exceptions
{
    public class BadRequestException : DataValidationException
    {
        public override int StatusCode => (int)HttpStatusCode.BadRequest;

        public BadRequestException(string message)
            : base(message) { }
    }
}
