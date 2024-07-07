using System.Net;

namespace DWorks.Domain.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(HttpStatusCode.NotFound, message)
        {
        }
    }
}
