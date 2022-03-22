using System.Net;

namespace Hl.Core.Application.Exceptions
{
    public class BadRequestException : DataValidationException
    {
        public override int StatusCode => (int)HttpStatusCode.BadRequest;

        public BadRequestException(string message)
            : base(message) { }
    }
}
