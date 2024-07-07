using Dworks.Application.Commands.ProjectCommands;
using Dworks.Application.Interfaces.Queries;
using DWorks.Tests.Mocks;
using Moq;
using System.Net;

namespace DWorks.Tests.Application.Commands.ProjectCommands
{
    public class GetProjectsCommandHandlerTest
    {
        private readonly GetProjectsCommandHandler getProjectsCommandHandlerMock;
        private readonly Mock<IProjectQueries> projectQueriesMock;
        private readonly Mock<IUserQueries> userQueriesMock;

        public GetProjectsCommandHandlerTest()
        {
            projectQueriesMock = new Mock<IProjectQueries>();
            userQueriesMock = new Mock<IUserQueries>();
            getProjectsCommandHandlerMock = new GetProjectsCommandHandler(projectQueriesMock.Object, userQueriesMock.Object);
        }

        [Fact]
        public async void should_handle_successfully()
        {
            userQueriesMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(true);
            projectQueriesMock.Setup(x => x.getAll()).ReturnsAsync(ProjectMocks.GetProjectsResponseMock());

            var request = ProjectMocks.GetProjectsRequestMock();
            var result = await getProjectsCommandHandlerMock.Handle(request, new CancellationToken());

            Assert.True(result.ValidationResult.Errors.Count().Equals(0));
        }

        [Fact]
        public async void should_return_error_not_found_user()
        {
            userQueriesMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(false);

            var request = ProjectMocks.GetProjectsRequestMock();
            var result = await getProjectsCommandHandlerMock.Handle(request, new CancellationToken());

            Assert.Equal(result.ValidationResult.Errors.First().ErrorCode, HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void should_throw_unexpected_and_throw_bad_request_exception()
        {
            userQueriesMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(true);
            projectQueriesMock.Setup(x => x.getAll()).ThrowsAsync(new Exception("Error"));

            var request = ProjectMocks.GetProjectsRequestMock();
            var result = await getProjectsCommandHandlerMock.Handle(request, new CancellationToken());

            Assert.Equal(result.ValidationResult.Errors.First().ErrorCode, HttpStatusCode.BadRequest.ToString());
        }
    }
}
