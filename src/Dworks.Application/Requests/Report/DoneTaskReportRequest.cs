using Dworks.Application.Responses.ApiResponse;
using Dworks.Application.Responses.Report;
using MediatR;

namespace Dworks.Application.Requests.Report
{
    public class DoneTaskReportRequest : ApiRequest, IRequest<ApiResponse<DoneTaskReportResponse>>
    {
    }
}
