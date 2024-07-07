using Dworks.Application.UseCases.Project;
using DWorks.Domain.Exceptions;
using DWorks.Domain.Interfaces.Repositories;
using DWorks.Tests.Mocks;
using Moq;

namespace DWorks.Tests.Application.UseCases.Project
{
    public class ProjectCreateUseCaseTest
    {
        private readonly ProjectCreateUseCase projectCreateUseCaseMock;
        private readonly Mock<IProjectRepository> projectRepositoryMock;

        public ProjectCreateUseCaseTest()
        {
            projectRepositoryMock = new Mock<IProjectRepository>();
            projectCreateUseCaseMock = new ProjectCreateUseCase(projectRepositoryMock.Object);
        }

        [Fact]
        public async void should_execute_successfully()
        {
            projectRepositoryMock.Setup(x => x.Insert(It.IsAny<Domain.Entities.Project>()))
                .Returns(System.Threading.Tasks.Task.FromResult<Domain.Entities.Project>(null));

            var request = ProjectMocks.ProjectMock();
            var myException = Record.ExceptionAsync(async () => await projectCreateUseCaseMock.execute(request));

            Assert.Null(myException.Result);
        }

        [Fact]
        public async void should_throw_unexpected_and_throw_bad_request_exception()
        {
            projectRepositoryMock.Setup(x => x.Insert(It.IsAny<Domain.Entities.Project>())).ThrowsAsync(new Exception("Error"));
            var request = ProjectMocks.ProjectMock();
            await Assert.ThrowsAsync<BadRequestException>(() => projectCreateUseCaseMock.execute(request));
        }
    }
}
