using Dworks.Application.UseCases.Task;
using DWorks.Domain.Exceptions;
using DWorks.Domain.Interfaces.Repositories;
using DWorks.Tests.Mocks;
using Moq;

namespace DWorks.Tests.Application.UseCases.Task
{
    public class TaskUpdateUseCaseTest
    {
        private readonly TaskUpdateUseCase taskUpdateUseCaseMock;
        private readonly Mock<ITaskRepository> taskRepositoryMock;

        public TaskUpdateUseCaseTest()
        {
            taskRepositoryMock = new Mock<ITaskRepository>();
            taskUpdateUseCaseMock = new TaskUpdateUseCase(taskRepositoryMock.Object);
        }

        [Fact]
        public async void should_execute_successfully()
        {
            taskRepositoryMock.Setup(x => x.Update(It.IsAny<Domain.Entities.Task>()))
                .Returns(System.Threading.Tasks.Task.FromResult<Domain.Entities.Task>(null));

            var request = TaskMocks.TaskMock();
            var myException = Record.ExceptionAsync(async () => await taskUpdateUseCaseMock.execute(request));

            Assert.Null(myException.Result);
        }

        [Fact]
        public async void should_throw_unexpected_and_throw_bad_request_exception()
        {
            taskRepositoryMock.Setup(x => x.Update(It.IsAny<Domain.Entities.Task>())).ThrowsAsync(new Exception("Error"));
            var request = TaskMocks.TaskMock();
            await Assert.ThrowsAsync<BadRequestException>(() => taskUpdateUseCaseMock.execute(request));
        }
    }
}
