using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using HR.Core.Domain.Enums;

namespace HR.Core.Application.Exceptions
{
    public class UnsupportedMediaTypeException : DataValidationException
    {
        public override int StatusCode => (int)HttpStatusCode.UnsupportedMediaType;

        public UnsupportedMediaTypeException(string message)
            : base(message) { }
    }
}
