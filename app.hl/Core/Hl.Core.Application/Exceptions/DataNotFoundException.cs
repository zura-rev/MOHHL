using System.Net;

namespace Hl.Core.Application.Exceptions
{
    public class DataNotFoundException : DataValidationException
    {
        public override int StatusCode => (int)HttpStatusCode.NotFound;

        public DataNotFoundException(string message)
            : base(message) { }
    }
}
