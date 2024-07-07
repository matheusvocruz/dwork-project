using System.Net;

namespace DWorks.Domain.Exceptions
{
    public class UnprocessableEntityException : BaseException
    {
        public UnprocessableEntityException(string message) : base(HttpStatusCode.UnprocessableEntity, message)
        {
        }
    }
}
