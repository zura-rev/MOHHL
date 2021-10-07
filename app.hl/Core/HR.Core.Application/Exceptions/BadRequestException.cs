using System.Net;

namespace HR.Core.Application.Exceptions
{
    public class BadRequestException : DataValidationException
    {
        public override int StatusCode => (int)HttpStatusCode.BadRequest;

        public BadRequestException(string message)
            : base(message) { }
    }
}
