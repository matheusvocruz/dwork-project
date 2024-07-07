using System.Net;

namespace DWorks.Domain.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message) : base(HttpStatusCode.BadRequest, message)
        {
        }
    }
}
