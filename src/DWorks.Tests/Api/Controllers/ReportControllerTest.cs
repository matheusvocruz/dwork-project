using Dworks.Application.Requests.Report;
using Dworks.Application.Requests.Task;
using DWorks.Api.Controllers;
using DWorks.Tests.Mocks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
namespace DWorks.Tests.Api.Controllers
{
    public class ReportControllerTest
    {
        private readonly ReportController reportController;
        private readonly Mock<ISender> senderMock;

        public ReportControllerTest()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["x-user-id"] = "1";

            senderMock = new Mock<ISender>();
            reportController = new ReportController(senderMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };
        }

        [Fact]
        public async void should_task_done_last_30_days_successfully()
        {
            senderMock.Setup(s => s.Send(It.IsAny<DoneTaskReportRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ReportMocks.ApiResponseDoneTaskReportResponseMock());

            var result = await reportController.taskDoneLast30Days();
            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
