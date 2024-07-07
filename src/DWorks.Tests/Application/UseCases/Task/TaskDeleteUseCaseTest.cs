using Dworks.Application.UseCases.Task;
using DWorks.Domain.Exceptions;
using DWorks.Domain.Interfaces.Repositories;
using DWorks.Tests.Mocks;
using Moq;

namespace DWorks.Tests.Application.UseCases.Task
{
    public class TaskDeleteUseCaseTest
    {
        private readonly TaskDeleteUseCase taskDeleteUseCaseMock;
        private readonly Mock<ITaskRepository> taskRepositoryMock;

        public TaskDeleteUseCaseTest()
        {
            taskRepositoryMock = new Mock<ITaskRepository>();
            taskDeleteUseCaseMock = new TaskDeleteUseCase(taskRepositoryMock.Object);
        }

        [Fact]
        public async void should_execute_successfully()
        {
            taskRepositoryMock.Setup(x => x.Delete(It.IsAny<Domain.Entities.Task>())).Verifiable();

            var request = TaskMocks.TaskMock();
            var myException = Record.Exception(() => taskDeleteUseCaseMock.execute(request));

            Assert.Null(myException);
        }

        [Fact]
        public async void should_throw_unexpected_and_throw_bad_request_exception()
        {
            taskRepositoryMock.Setup(x => x.Delete(It.IsAny<Domain.Entities.Task>())).Throws(new Exception("Error"));
            var request = TaskMocks.TaskMock();
            Assert.Throws<BadRequestException>(() => taskDeleteUseCaseMock.execute(request));
        }
    }
}
