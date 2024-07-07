using Dworks.Application.Commands.ReportCommands;
using Dworks.Application.Interfaces.Queries;
using DWorks.Tests.Mocks;
using Moq;
using System.Net;

namespace DWorks.Tests.Application.Commands.ReportCommands
{
    public class DoneTaskReportCommandHandlerTest
    {
        private readonly DoneTaskReportCommandHandler doneTaskReportCommandHandlerTestMock;
        private readonly Mock<ITaskQueries> taskQueriesMock;
        private readonly Mock<IUserQueries> userQueriesMock;

        public DoneTaskReportCommandHandlerTest()
        {
            taskQueriesMock = new Mock<ITaskQueries>();
            userQueriesMock = new Mock<IUserQueries>();
            doneTaskReportCommandHandlerTestMock = new DoneTaskReportCommandHandler(taskQueriesMock.Object,
                userQueriesMock.Object);
        }

        [Fact]
        public async void should_handle_successfully()
        {
            userQueriesMock.Setup(x => x.getById(It.IsAny<long>())).ReturnsAsync(UserMocks.ManagerUserMock());
            taskQueriesMock.Setup(x => x.getDoneReport()).ReturnsAsync(ReportMocks.DoneTaskReportResponseMock());

            var request = ReportMocks.DoneTaskReportRequestMock();
            var result = await doneTaskReportCommandHandlerTestMock.Handle(request, new CancellationToken());

            Assert.True(result.ValidationResult.Errors.Count().Equals(0));
        }

        [Fact]
        public async void should_return_error_not_found_user()
        {
            userQueriesMock.Setup(x => x.getById(It.IsAny<long>())).ReturnsAsync(() => null);

            var request = ReportMocks.DoneTaskReportRequestMock();
            var result = await doneTaskReportCommandHandlerTestMock.Handle(request, new CancellationToken());

            Assert.Equal(result.ValidationResult.Errors.First().ErrorCode, HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void should_return_error_not_allowed_user()
        {
            userQueriesMock.Setup(x => x.getById(It.IsAny<long>())).ReturnsAsync(UserMocks.UserMock());

            var request = ReportMocks.DoneTaskReportRequestMock();
            var result = await doneTaskReportCommandHandlerTestMock.Handle(request, new CancellationToken());

            Assert.Equal(result.ValidationResult.Errors.First().ErrorCode, HttpStatusCode.MethodNotAllowed.ToString());
        }

        [Fact]
        public async void should_return_error_bad_request_unexpected_throw()
        {
            userQueriesMock.Setup(x => x.getById(It.IsAny<long>())).ReturnsAsync(UserMocks.ManagerUserMock());
            taskQueriesMock.Setup(x => x.getDoneReport()).ThrowsAsync(new Exception("Lock"));

            var request = ReportMocks.DoneTaskReportRequestMock();
            var result = await doneTaskReportCommandHandlerTestMock.Handle(request, new CancellationToken());

            Assert.Equal(result.ValidationResult.Errors.First().ErrorCode, HttpStatusCode.BadRequest.ToString());
        }
    }
}
