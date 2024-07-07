using System.Net;

namespace DWorks.Domain.Exceptions
{
    public class NotAllowedException : BaseException
    {
        public NotAllowedException(string message) : base(HttpStatusCode.MethodNotAllowed, message)
        {
        }
    }
}
