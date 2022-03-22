using System.Net;

namespace Hl.Core.Application.Exceptions
{
    public class DataAlreadyExistsException : DataValidationException
    {
        public override int StatusCode => (int)HttpStatusCode.BadRequest;

        public DataAlreadyExistsException(string message)
          : base(message) { }
    }
}
