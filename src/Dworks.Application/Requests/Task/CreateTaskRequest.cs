using Dworks.Application.Responses.ApiResponse;
using Dworks.Application.Responses.Task;
using DWorks.Domain.Enums.Task;
using MediatR;

namespace Dworks.Application.Requests.Task
{
    public class CreateTaskRequest : ApiRequest, IRequest<ApiResponse<CreateTaskResponse>>
    {
        public required string Tittle { get; set; }
        public required string Description { get; set; }
        public required TaskPriorityEnum Priority { get; set; }
        public required long ProjectId { get; set; }
        public required DateTime ExpiresAt { get; set; }
    }
}
