using Dworks.Application.Responses.ApiResponse;
using MediatR;

namespace Dworks.Application.Requests.Project
{
    public class DeleteProjectRequest : ApiRequest, IRequest<ApiResponse<UnitResponse>>
    {
        public long Id { get; set; }
    }
}
