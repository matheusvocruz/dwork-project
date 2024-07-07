using Dworks.Application.Responses.ApiResponse;
using Dworks.Application.Responses.Project;
using MediatR;
using System.Text.Json.Serialization;

namespace Dworks.Application.Requests.Project
{
    public class CreateProjectRequest : ApiRequest, IRequest<ApiResponse<CreateProjectResponse>>
    {
        public required string Name { get; set; }
    }
}
