using Dworks.Application.Commands.TaskCommands;
using Dworks.Application.Interfaces.Queries;
using Dworks.Application.Interfaces.UseCases;
using DWorks.Domain.Entities;
using DWorks.Tests.Mocks;
using Moq;
using System.Net;

namespace DWorks.Tests.Application.Commands.TaskCommands
{
    public class CreateTaskCommentCommandHandlerTest
    {
        private readonly CreateTaskCommentCommandHandler createTaskCommentCommandHandler;
        private readonly Mock<ICommentCreateUseCase> commentCreateUseCaseMock;
        private readonly Mock<ITaskQueries> taskQueriesMock;
        private readonly Mock<ILogCreateUseCase> logCreateUseCase;
        private readonly Mock<IUserQueries> userQueriesMock;

        public CreateTaskCommentCommandHandlerTest()
        {
            commentCreateUseCaseMock = new Mock<ICommentCreateUseCase>();
            taskQueriesMock = new Mock<ITaskQueries>();
            logCreateUseCase = new Mock<ILogCreateUseCase>();
            userQueriesMock = new Mock<IUserQueries>();
            createTaskCommentCommandHandler = new CreateTaskCommentCommandHandler(commentCreateUseCaseMock.Object,
                taskQueriesMock.Object, logCreateUseCase.Object, userQueriesMock.Object);
        }

        [Fact]
        public async void should_handle_successfully()
        {
            userQueriesMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(true);
            taskQueriesMock.Setup(x => x.getById(It.IsAny<long>())).ReturnsAsync(TaskMocks.TaskMock());

            var request = CommentMocks.CreateTaskCommentRequestMock();
            var result = await createTaskCommentCommandHandler.Handle(request, new CancellationToken());

            commentCreateUseCaseMock.Verify(x => x.execute(It.IsAny<Comment>()), Times.AtLeastOnce);
            logCreateUseCase.Verify(x => x.execute(It.IsAny<List<Log>>()), Times.AtLeastOnce);

            Assert.True(result.ValidationResult.Errors.Count().Equals(0));
        }

        [Fact]
        public async void should_return_error_not_found_user()
        {
            userQueriesMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(false);

            var request = TaskMocks.CreateTaskCommentRequestMock();
            var result = await createTaskCommentCommandHandler.Handle(request, new CancellationToken());

            Assert.Equal(result.ValidationResult.Errors.First().ErrorCode, HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void should_return_error_not_found_task()
        {
            userQueriesMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(true);
            taskQueriesMock.Setup(x => x.getById(It.IsAny<long>())).ReturnsAsync(() => null);

            var request = TaskMocks.CreateTaskCommentRequestMock();
            var result = await createTaskCommentCommandHandler.Handle(request, new CancellationToken());

            Assert.Equal(result.ValidationResult.Errors.First().ErrorCode, HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void should_return_error_bad_request_unexpected_throw()
        {
            userQueriesMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(true);
            taskQueriesMock.Setup(x => x.getById(It.IsAny<long>())).ThrowsAsync(new Exception("Lock"));

            var request = TaskMocks.CreateTaskCommentRequestMock();
            var result = await createTaskCommentCommandHandler.Handle(request, new CancellationToken());

            Assert.Equal(result.ValidationResult.Errors.First().ErrorCode, HttpStatusCode.BadRequest.ToString());
        }
    }
}
