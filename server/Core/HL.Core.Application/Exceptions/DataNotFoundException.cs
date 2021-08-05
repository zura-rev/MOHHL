using System;
using System.Net;
using HL.Core.Domain.Enums;

namespace HL.Core.Application.Exceptions
{
    public class DataNotFoundException : DataValidationException
    {
        public override int StatusCode => (int)HttpStatusCode.NotFound;

        public DataNotFoundException(string message)
            : base(message) { }
    }
}
