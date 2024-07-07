using Dworks.Application.Responses.ApiResponse;
using MediatR;

namespace Dworks.Application.Requests.Task
{
    public class DeleteTaskRequest : ApiRequest, IRequest<ApiResponse<UnitResponse>>
    {
        public long Id { get; set; }
    }
}
