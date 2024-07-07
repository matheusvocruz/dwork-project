using Dworks.Application.Requests.Report;
using Dworks.Application.Responses.Report;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DWorks.Api.Controllers
{
    public class ReportController : BaseController
    {
        private readonly ISender _mediator;

        public ReportController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("taskDoneLast30Days")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DoneTaskReportResponse))]
        public async Task<IActionResult> taskDoneLast30Days()
        {
            var request = new DoneTaskReportRequest();
            buildUserId(request);
            return CustomResponse(await _mediator.Send(request));
        }    
    }
}
