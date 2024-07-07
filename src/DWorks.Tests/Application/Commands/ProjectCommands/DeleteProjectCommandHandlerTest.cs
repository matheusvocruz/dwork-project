using Dworks.Application.Commands.ProjectCommands;
using Dworks.Application.Interfaces.Queries;
using Dworks.Application.Interfaces.UseCases;
using DWorks.Domain.Entities;
using DWorks.Tests.Mocks;
using Moq;
using System.Net;

namespace DWorks.Tests.Application.Commands.ProjectCommands
{
    public class DeleteProjectCommandHandlerTest
    {
        private readonly DeleteProjectCommandHandler createProjectCommandHandlerMock;
        private readonly Mock<IProjectQueries> projectQueriesMock;
        private readonly Mock<ITaskQueries> taskQueriesMock;
        private readonly Mock<IProjectDeleteUseCase> projectDeleteUseCaseMock;
        private readonly Mock<IUserQueries> userQueriesMock;

        public DeleteProjectCommandHandlerTest()
        {
            projectQueriesMock = new Mock<IProjectQueries>();
            taskQueriesMock = new Mock<ITaskQueries>();
            projectDeleteUseCaseMock = new Mock<IProjectDeleteUseCase>();
            userQueriesMock = new Mock<IUserQueries>();
            createProjectCommandHandlerMock = new DeleteProjectCommandHandler(projectQueriesMock.Object,
                taskQueriesMock.Object, projectDeleteUseCaseMock.Object, userQueriesMock.Object);
        }


        [Fact]
        public async void should_handle_successfully()
        {
            userQueriesMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(true);
            projectQueriesMock.Setup(x => x.getById(It.IsAny<long>())).ReturnsAsync(ProjectMocks.ProjectMock());
            taskQueriesMock.Setup(x => x.hasPendingTaskByProject(It.IsAny<long>())).ReturnsAsync(false);

            var request = ProjectMocks.DeleteProjectRequestMock();
            var result = await createProjectCommandHandlerMock.Handle(request, new CancellationToken());

            projectDeleteUseCaseMock.Verify(x => x.execute(It.IsAny<Project>()), Times.AtLeastOnce);

            Assert.True(result.ValidationResult.Errors.Count().Equals(0));
        }

        [Fact]
        public async void should_return_error_not_found_user()
        {
            userQueriesMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(false);

            var request = ProjectMocks.DeleteProjectRequestMock();
            var result = await createProjectCommandHandlerMock.Handle(request, new CancellationToken());

            Assert.Equal(result.ValidationResult.Errors.First().ErrorCode, HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void should_return_error_not_found_task()
        {
            userQueriesMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(true);
            projectQueriesMock.Setup(x => x.getById(It.IsAny<long>())).ReturnsAsync(() => null);

            var request = ProjectMocks.DeleteProjectRequestMock();
            var result = await createProjectCommandHandlerMock.Handle(request, new CancellationToken());

            Assert.Equal(result.ValidationResult.Errors.First().ErrorCode, HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void should_return_error_bad_request_pending_tasks()
        {
            userQueriesMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(true);
            projectQueriesMock.Setup(x => x.getById(It.IsAny<long>())).ReturnsAsync(ProjectMocks.ProjectMock());
            taskQueriesMock.Setup(x => x.hasPendingTaskByProject(It.IsAny<long>())).ReturnsAsync(true);

            var request = ProjectMocks.DeleteProjectRequestMock();
            var result = await createProjectCommandHandlerMock.Handle(request, new CancellationToken());

            Assert.Equal(result.ValidationResult.Errors.First().ErrorCode, HttpStatusCode.BadRequest.ToString());
        }

        [Fact]
        public async void should_return_error_bad_request_unexpected_throw()
        {
            userQueriesMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(true);
            projectQueriesMock.Setup(x => x.getById(It.IsAny<long>())).ThrowsAsync(new Exception("Lock"));

            var request = ProjectMocks.DeleteProjectRequestMock();
            var result = await createProjectCommandHandlerMock.Handle(request, new CancellationToken());

            Assert.Equal(result.ValidationResult.Errors.First().ErrorCode, HttpStatusCode.BadRequest.ToString());
        }
    }
}
