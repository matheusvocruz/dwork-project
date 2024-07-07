using Dworks.Application.Responses.ApiResponse;
using Dworks.Application.Responses.Task;
using MediatR;

namespace Dworks.Application.Requests.Task
{
    public class GetTasksByProjectRequest : ApiRequest, IRequest<ApiResponse<GetTasksResponse>>
    {
        public long Id { get; set; }
    }
}
