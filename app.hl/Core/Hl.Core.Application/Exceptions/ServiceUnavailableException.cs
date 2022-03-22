using System.Net;

namespace Hl.Core.Application.Exceptions
{
    public class ServiceUnavailableException : DataValidationException
    {
        public override int StatusCode => (int)HttpStatusCode.ServiceUnavailable;

        public ServiceUnavailableException(string message)
            : base(message) { }
    }
}
