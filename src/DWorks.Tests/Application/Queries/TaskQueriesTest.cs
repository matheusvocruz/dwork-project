using AutoMapper;
using Dworks.Application.Mapper;
using Dworks.Application.Queries;
using DWorks.Domain.Exceptions;
using DWorks.Domain.Interfaces.Repositories;
using DWorks.Tests.Mocks;
using Moq;

namespace DWorks.Tests.Application.Queries
{
    public class TaskQueriesTest
    {
        private readonly TaskQueries taskQueries;
        private readonly Mock<ITaskRepository> taskRepositoryMock;
        private readonly IMapper mapperMock;

        public TaskQueriesTest()
        {
            taskRepositoryMock = new Mock<ITaskRepository>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DomainToApplicationMappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            mapperMock = mapper;
            taskQueries = new TaskQueries(taskRepositoryMock.Object, mapperMock);
        }

        [Fact]
        public async void should_get_by_id_successfully()
        {
            taskRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(TaskMocks.TaskMock());

            var result = await taskQueries.getById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async void should_get_by_id_throw_bad_request_when_unexpected_throw()
        {
            taskRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>())).ThrowsAsync(new Exception("Lock"));
            await Assert.ThrowsAsync<BadRequestException>(() => taskQueries.getById(1));
        }

        [Fact]
        public async void should_has_pending_task_by_project_successfully()
        {
            taskRepositoryMock.Setup(x => x.hasPendingTaskByProject(It.IsAny<long>())).ReturnsAsync(true);

            var result = await taskQueries.hasPendingTaskByProject(1);

            Assert.True(result);
        }

        [Fact]
        public async void should_has_pending_task_by_project_throw_bad_request_when_unexpected_throw()
        {
            taskRepositoryMock.Setup(x => x.hasPendingTaskByProject(It.IsAny<long>())).ThrowsAsync(new Exception("Lock"));
            await Assert.ThrowsAsync<BadRequestException>(() => taskQueries.hasPendingTaskByProject(1));
        }

        [Fact]
        public async void should_get_count_by_project_successfully()
        {
            taskRepositoryMock.Setup(x => x.getCountByProject(It.IsAny<long>())).ReturnsAsync(1);

            var result = await taskQueries.getCountByProject(1);

            Assert.Equal(1, result);
        }

        [Fact]
        public async void should_get_count_by_projectthrow_bad_request_when_unexpected_throw()
        {
            taskRepositoryMock.Setup(x => x.getCountByProject(It.IsAny<long>())).ThrowsAsync(new Exception("Lock"));
            await Assert.ThrowsAsync<BadRequestException>(() => taskQueries.getCountByProject(1));
        }

        [Fact]
        public async void should_get_all_by_project_successfully()
        {
            taskRepositoryMock.Setup(x => x.getAllByProject(It.IsAny<long>())).ReturnsAsync(TaskMocks.TaskListMock());

            var result = await taskQueries.getAllByProject(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async void should_get_all_by_projectthrow_bad_request_when_unexpected_throw()
        {
            taskRepositoryMock.Setup(x => x.getAllByProject(It.IsAny<long>())).ThrowsAsync(new Exception("Lock"));
            await Assert.ThrowsAsync<BadRequestException>(() => taskQueries.getAllByProject(1));
        }

        [Fact]
        public async void should_get_done_report_successfully()
        {
            taskRepositoryMock.Setup(x => x.getDoneReport()).ReturnsAsync(ReportMocks.DoneTaskDtoListMock());

            var result = await taskQueries.getDoneReport();

            Assert.NotNull(result);
        }

        [Fact]
        public async void should_get_done_report_throw_bad_request_when_unexpected_throw()
        {
            taskRepositoryMock.Setup(x => x.getDoneReport()).ThrowsAsync(new Exception("Lock"));
            await Assert.ThrowsAsync<BadRequestException>(() => taskQueries.getDoneReport());
        }
    }
}
