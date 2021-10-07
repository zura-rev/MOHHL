using System;
using System.Net;
using HR.Core.Domain.Enums;

namespace HR.Core.Application.Exceptions
{
    public class DataAlreadyExistsException : DataValidationException
    {
        public override int StatusCode => (int)HttpStatusCode.BadRequest;

        public DataAlreadyExistsException(string message)
          : base(message) { }
    }
}
