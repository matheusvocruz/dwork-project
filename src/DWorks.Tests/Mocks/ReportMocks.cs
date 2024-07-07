using Dworks.Application.Requests.Report;
using Dworks.Application.Responses.ApiResponse;
using Dworks.Application.Responses.Report;
using DWorks.Domain.ValueObjects;
using FluentValidation.Results;

namespace DWorks.Tests.Mocks
{
    public static class ReportMocks
    {
        public static ApiResponse<DoneTaskReportResponse> ApiResponseDoneTaskReportResponseMock()
        {
            ValidationResult validationResult = new ValidationResult();
            validationResult.Errors = new List<ValidationFailure> { };

            return new ApiResponse<DoneTaskReportResponse>
            {
                Data = DoneTaskReportResponseMock(),
                ValidationResult = validationResult
            };
        }

        public static DoneTaskReportResponse DoneTaskReportResponseMock()
            => new DoneTaskReportResponse { Report = new List<DoneTask> { DoneTaskMock() }};

        public static DoneTask DoneTaskMock()
            => new DoneTask { Total = 1, UserId = 1, UserName = "UserName" };

        public static DoneTaskReportRequest DoneTaskReportRequestMock()
            => new DoneTaskReportRequest { UserId = 1 };

        public static List<DoneTaskDto> DoneTaskDtoListMock()
            => new List<DoneTaskDto> { DoneTaskDtoMock() };

        public static DoneTaskDto DoneTaskDtoMock()
            => new DoneTaskDto { Total = 1, UserId = 1, UserName = "UserName" };
    }
}
