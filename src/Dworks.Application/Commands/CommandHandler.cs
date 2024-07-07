using Dworks.Application.Responses.ApiResponse;

namespace Dworks.Application.Commands
{
    public abstract class CommandHandler<T> : ResponseHandler<T> where T : class
    {
    }
}
