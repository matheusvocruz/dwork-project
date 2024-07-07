using Dworks.Application.Interfaces.Queries;
using Dworks.Application.Requests.Project;
using Dworks.Application.Requests.Task;
using Dworks.Application.Responses.Project;
using Dworks.Application.Responses.Task;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DWorks.Api.Controllers
{
    public class ProjectController : BaseController
    {
        private readonly ISender _mediator;

        public ProjectController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProjectsResponse))]
        public async Task<IActionResult> getProjectsAsync()
        {
            var request = new GetProjectsRequest();
            buildUserId(request);
            return CustomResponse(await _mediator.Send(request));
        }
             

        [HttpGet("{Id}/tasks")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTasksResponse))]
        public async Task<IActionResult> getTasksByProjectAsync([FromRoute] GetTasksByProjectRequest request)
        {
            buildUserId(request);
            return CustomResponse(await _mediator.Send(request));
        }
            

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateProjectResponse))]
        public async Task<IActionResult> createAsync([FromBody] CreateProjectRequest request)
        {
            buildUserId(request);
            return CustomResponse(await _mediator.Send(request));
        }
            

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> deleteAsync([FromBody] DeleteProjectRequest request)
        {
            buildUserId(request);
            return CustomResponse(await _mediator.Send(request));
        }
    }
}
