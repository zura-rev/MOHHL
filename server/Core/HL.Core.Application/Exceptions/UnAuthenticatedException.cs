using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using HL.Core.Domain.Enums;

namespace HL.Core.Application.Exceptions
{
    public class UnAuthenticatedException : DataValidationException
    {
        public override int StatusCode => (int)HttpStatusCode.Unauthorized;

        public UnAuthenticatedException(string message)
            : base(message) { }
    }
}
