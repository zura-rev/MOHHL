using System.Net;

namespace Hl.Core.Application.Exceptions
{
    public class UnAuthenticatedException : DataValidationException
    {
        public override int StatusCode => (int)HttpStatusCode.Unauthorized;

        public UnAuthenticatedException(string message)
            : base(message) { }
    }
}
