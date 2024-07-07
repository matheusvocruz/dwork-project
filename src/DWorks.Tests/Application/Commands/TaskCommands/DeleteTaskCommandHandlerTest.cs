using Dworks.Application.Commands.TaskCommands;
using Dworks.Application.Interfaces.Queries;
using Dworks.Application.Interfaces.UseCases;
using DWorks.Domain.Interfaces.Repositories;
using DWorks.Tests.Mocks;
using Moq;
using System.Net;

namespace DWorks.Tests.Application.Commands.TaskCommands
{
    public class DeleteTaskCommandHandlerTest
    {
        private readonly DeleteTaskCommandHandler deleteTaskCommandHandlerMock;
        private readonly Mock<ITaskDeleteUseCase> taskDeleteUseCaseMock;
        private readonly Mock<ITaskQueries> taskQueriesMock;
        private readonly Mock<IUserQueries> userQueriesMock;

        public DeleteTaskCommandHandlerTest()
        {
            taskDeleteUseCaseMock = new Mock<ITaskDeleteUseCase>();
            taskQueriesMock = new Mock<ITaskQueries>();
            userQueriesMock = new Mock<IUserQueries>();
            deleteTaskCommandHandlerMock = new DeleteTaskCommandHandler(taskDeleteUseCaseMock.Object, 
                taskQueriesMock.Object, userQueriesMock.Object);
        }

        [Fact]
        public async void should_handle_successfully()
        {
            userQueriesMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(true);
            taskQueriesMock.Setup(x => x.getById(It.IsAny<long>())).ReturnsAsync(TaskMocks.TaskMock());

            var request = TaskMocks.DeleteTaskRequestMock();
            var result = await deleteTaskCommandHandlerMock.Handle(request, new CancellationToken());

            taskDeleteUseCaseMock.Verify(x => x.execute(It.IsAny<Domain.Entities.Task>()), Times.AtLeastOnce);

            Assert.True(result.ValidationResult.Errors.Count().Equals(0));
        }

        [Fact]
        public async void should_return_error_not_found_user()
        {
            userQueriesMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(false);

            var request = TaskMocks.DeleteTaskRequestMock();
            var result = await deleteTaskCommandHandlerMock.Handle(request, new CancellationToken());

            Assert.Equal(result.ValidationResult.Errors.First().ErrorCode, HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void should_return_error_not_found_task()
        {
            userQueriesMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(true);
            taskQueriesMock.Setup(x => x.getById(It.IsAny<long>())).ReturnsAsync(() => null);

            var request = TaskMocks.DeleteTaskRequestMock();
            var result = await deleteTaskCommandHandlerMock.Handle(request, new CancellationToken());

            Assert.Equal(result.ValidationResult.Errors.First().ErrorCode, HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void should_return_error_bad_request_unexpected_throw()
        {
            userQueriesMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(true);
            taskQueriesMock.Setup(x => x.getById(It.IsAny<long>())).ThrowsAsync(new Exception("Lock"));

            var request = TaskMocks.DeleteTaskRequestMock();
            var result = await deleteTaskCommandHandlerMock.Handle(request, new CancellationToken());

            Assert.Equal(result.ValidationResult.Errors.First().ErrorCode, HttpStatusCode.BadRequest.ToString());
        }
    }
}
