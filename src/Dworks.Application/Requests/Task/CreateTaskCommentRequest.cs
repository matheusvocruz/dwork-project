using Dworks.Application.Responses.ApiResponse;
using MediatR;

namespace Dworks.Application.Requests.Task
{
    public class CreateTaskCommentRequest : ApiRequest, IRequest<ApiResponse<UnitResponse>>
    {
        public long TaskId { get; set; }
        public required string Comment { get; set; }
    }
}
