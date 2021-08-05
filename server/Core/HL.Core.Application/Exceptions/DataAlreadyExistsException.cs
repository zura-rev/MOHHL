using System;
using System.Net;
using HL.Core.Domain.Enums;

namespace HL.Core.Application.Exceptions
{
    public class DataAlreadyExistsException : DataValidationException
    {
        public override int StatusCode => (int)HttpStatusCode.BadRequest;

        public DataAlreadyExistsException(string message)
          : base(message) { }
    }
}
