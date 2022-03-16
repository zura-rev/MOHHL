using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Tasks.Core.Domain.Enums;

namespace Tasks.Core.Application.Exceptions
{
    public class UnsupportedMediaTypeException : DataValidationException
    {
        public override int StatusCode => (int)HttpStatusCode.UnsupportedMediaType;

        public UnsupportedMediaTypeException(string message)
            : base(message) { }
    }
}
