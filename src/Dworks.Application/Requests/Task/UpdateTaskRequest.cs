using Dworks.Application.Responses.ApiResponse;
using DWorks.Domain.Enums.Task;
using MediatR;

namespace Dworks.Application.Requests.Task
{
    public class UpdateTaskRequest : ApiRequest, IRequest<ApiResponse<UnitResponse>>
    {
        public long Id { get; set; }
        public required string Tittle { get; set; }
        public required string Description { get; set; }
        public TaskStatusEnum Status { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
