using Dworks.Application.Commands.TaskCommands;
using Dworks.Application.Interfaces.Queries;
using Dworks.Application.Interfaces.UseCases;
using DWorks.Domain.Entities;
using DWorks.Tests.Mocks;
using Moq;
using System.Net;

namespace DWorks.Tests.Application.Commands.TaskCommands
{
    public class UpdateTaskCommandHandlerTest
    {
        private readonly UpdateTaskCommandHandler updateTaskCommandHandler;
        private readonly Mock<ITaskUpdateUseCase> taskUpdateUseCaseMock;
        private readonly Mock<ITaskQueries> taskQueriesMock;
        private readonly Mock<ILogCreateUseCase> logCreateUseCaseMock;
        private readonly Mock<IUserQueries> userQueriesMock;

        public UpdateTaskCommandHandlerTest()
        {
            taskUpdateUseCaseMock = new Mock<ITaskUpdateUseCase>();
            taskQueriesMock = new Mock<ITaskQueries>();
            logCreateUseCaseMock = new Mock<ILogCreateUseCase>();
            userQueriesMock = new Mock<IUserQueries>();
            updateTaskCommandHandler = new UpdateTaskCommandHandler(taskUpdateUseCaseMock.Object, taskQueriesMock.Object,
                logCreateUseCaseMock.Object, userQueriesMock.Object);
        }

        [Fact]
        public async void should_handle_successfully()
        {
            userQueriesMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(true);
            taskQueriesMock.Setup(x => x.getById(It.IsAny<long>())).ReturnsAsync(TaskMocks.TaskMock());

            var request = TaskMocks.UpdateTaskRequestMock();
            var result = await updateTaskCommandHandler.Handle(request, new CancellationToken());

            taskUpdateUseCaseMock.Verify(x => x.execute(It.IsAny<Domain.Entities.Task>()), Times.AtLeastOnce);
            logCreateUseCaseMock.Verify(x => x.execute(It.IsAny<List<Log>>()), Times.AtLeastOnce);

            Assert.True(result.ValidationResult.Errors.Count().Equals(0));
        }

        [Fact]
        public async void should_return_error_not_found_user()
        {
            userQueriesMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(false);

            var request = TaskMocks.UpdateTaskRequestMock();
            var result = await updateTaskCommandHandler.Handle(request, new CancellationToken());

            Assert.Equal(result.ValidationResult.Errors.First().ErrorCode, HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void should_handle_without_update_successfully()
        {
            userQueriesMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(true);
            taskQueriesMock.Setup(x => x.getById(It.IsAny<long>())).ReturnsAsync(TaskMocks.TaskMock());

            var request = TaskMocks.UpdateTaskRequestWithoutUpdateMock();
            var result = await updateTaskCommandHandler.Handle(request, new CancellationToken());

            Assert.True(result.ValidationResult.Errors.Count().Equals(0));
        }

        [Fact]
        public async void should_return_error_not_found_task()
        {
            userQueriesMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(true);
            taskQueriesMock.Setup(x => x.getById(It.IsAny<long>())).ReturnsAsync(() => null);

            var request = TaskMocks.UpdateTaskRequestMock();
            var result = await updateTaskCommandHandler.Handle(request, new CancellationToken());

            Assert.Equal(result.ValidationResult.Errors.First().ErrorCode, HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void should_return_error_bad_request_unexpected_throw()
        {
            userQueriesMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(true);
            taskQueriesMock.Setup(x => x.getById(It.IsAny<long>())).ThrowsAsync(new Exception("Lock"));

            var request = TaskMocks.UpdateTaskRequestMock();
            var result = await updateTaskCommandHandler.Handle(request, new CancellationToken());

            Assert.Equal(result.ValidationResult.Errors.First().ErrorCode, HttpStatusCode.BadRequest.ToString());
        }
    }
}
