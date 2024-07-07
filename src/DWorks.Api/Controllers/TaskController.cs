using Dworks.Application.Requests.Task;
using Dworks.Application.Responses.Task;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DWorks.Api.Controllers
{
    public class TaskController : BaseController
    {
        private readonly ISender _mediator;

        public TaskController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateTaskResponse))]
        public async Task<IActionResult> createAsync([FromBody] CreateTaskRequest request)
        {
            buildUserId(request);
            return CustomResponse(await _mediator.Send(request));
        }

        [HttpPost("comment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateTaskResponse))]
        public async Task<IActionResult> createCommentAsync([FromBody] CreateTaskCommentRequest request)
        {
            buildUserId(request);
            return CustomResponse(await _mediator.Send(request));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> updateAsync([FromBody] UpdateTaskRequest request)
        {
            buildUserId(request);
            return CustomResponse(await _mediator.Send(request));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> deleteAsync([FromBody] DeleteTaskRequest request)
        {
            buildUserId(request);
            return CustomResponse(await _mediator.Send(request));
        }
    }
}
