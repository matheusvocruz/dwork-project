using Dworks.Application.Responses.ApiResponse;
using Dworks.Application.Responses.Project;
using MediatR;

namespace Dworks.Application.Requests.Project
{
    public class GetProjectsRequest : ApiRequest, IRequest<ApiResponse<GetProjectsResponse>>
    {
    }
}
