using System;
using System.Net;
using Tasks.Core.Domain.Enums;

namespace Tasks.Core.Application.Exceptions
{
    public class DataAlreadyExistsException : DataValidationException
    {
        public override int StatusCode => (int)HttpStatusCode.BadRequest;

        public DataAlreadyExistsException(string message)
          : base(message) { }
    }
}
