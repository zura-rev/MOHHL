using System.Net;

namespace Hl.Core.Application.Exceptions
{
    public class PersonRejectedException : DataValidationException
    {
        public override int StatusCode => (int)HttpStatusCode.NotAcceptable;

        public PersonRejectedException(string message)
            : base(message) { }
    }
}
