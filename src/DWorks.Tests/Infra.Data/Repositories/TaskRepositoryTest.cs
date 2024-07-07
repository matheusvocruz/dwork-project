using DWorks.Infra.Data.Context;
using DWorks.Infra.Data.Repositories;
using DWorks.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
namespace DWorks.Tests.Infra.Data.Repositories
{

    public class TaskRepositoryTest : IDisposable
    {
        private readonly TaskRepository taskRepositoryMock;
        private readonly ProjectContext contextMock;

        public TaskRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ProjectContext>()
                .UseInMemoryDatabase(databaseName: "ProjectTaskDatabase")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution)
                .EnableSensitiveDataLogging()
                .Options;
            contextMock = new ProjectContext(options);
            contextMock.Database.EnsureDeleted();

            contextMock.Tasks.Add(TaskMocks.TaskCreateMock());

            contextMock.SaveChanges();

            taskRepositoryMock = new TaskRepository(contextMock);
        }

        [Fact]
        public async void should_has_pending_task_by_project_successfully()
        {
            Assert.True(await taskRepositoryMock.hasPendingTaskByProject(1));
        }

        [Fact]
        public async void should_get_count_by_project_successfully()
        {
            Assert.True(await taskRepositoryMock.getCountByProject(1) > 0);
        }

        [Fact]
        public async void should_get_all_by_project_successfully()
        {
            Assert.NotNull(await taskRepositoryMock.getAllByProject(1));
        }

        public void Dispose()
        {
        }
    }
}
