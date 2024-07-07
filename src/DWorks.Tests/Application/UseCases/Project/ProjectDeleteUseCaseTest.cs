using Dworks.Application.UseCases.Project;
using DWorks.Domain.Exceptions;
using DWorks.Domain.Interfaces.Repositories;
using DWorks.Tests.Mocks;
using Moq;

namespace DWorks.Tests.Application.UseCases.Project
{
    public class ProjectDeleteUseCaseTest
    {
        private readonly ProjectDeleteUseCase projectDeleteUseCaseMock;
        private readonly Mock<IProjectRepository> projectRepositoryMock;

        public ProjectDeleteUseCaseTest()
        {
            projectRepositoryMock = new Mock<IProjectRepository>();
            projectDeleteUseCaseMock = new ProjectDeleteUseCase(projectRepositoryMock.Object);
        }

        [Fact]
        public async void should_execute_successfully()
        {
            projectRepositoryMock.Setup(x => x.Delete(It.IsAny<Domain.Entities.Project>())).Verifiable();

            var request = ProjectMocks.ProjectMock();
            var myException = Record.Exception(() => projectDeleteUseCaseMock.execute(request));

            Assert.Null(myException);
        }

        [Fact]
        public async void should_throw_unexpected_and_throw_bad_request_exception()
        {
            projectRepositoryMock.Setup(x => x.Delete(It.IsAny<Domain.Entities.Project>())).Throws(new Exception("Error"));
            var request = ProjectMocks.ProjectMock();
            Assert.Throws<BadRequestException>(() => projectDeleteUseCaseMock.execute(request));
        }
    }
}
