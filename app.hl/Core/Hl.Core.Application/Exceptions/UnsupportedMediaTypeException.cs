using System.Net;

namespace Hl.Core.Application.Exceptions
{
    public class UnsupportedMediaTypeException : DataValidationException
    {
        public override int StatusCode => (int)HttpStatusCode.UnsupportedMediaType;

        public UnsupportedMediaTypeException(string message)
            : base(message) { }
    }
}
