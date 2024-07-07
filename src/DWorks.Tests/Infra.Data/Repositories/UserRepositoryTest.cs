using DWorks.Domain.Entities;
using DWorks.Infra.Data.Context;
using DWorks.Infra.Data.Repositories;
using DWorks.Tests.Mocks;
using Microsoft.EntityFrameworkCore;

namespace DWorks.Tests.Infra.Data.Repositories
{
    public class UserRepositoryTest : IDisposable
    {
        private readonly UserRepository userRepositoryMock;
        private readonly ProjectContext contextMock;

        public UserRepositoryTest() 
        {
            var options = new DbContextOptionsBuilder<ProjectContext>()
                .UseInMemoryDatabase(databaseName: "ProjectUserDatabase")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution)
                .EnableSensitiveDataLogging()
                .Options;
            contextMock = new ProjectContext(options);
            contextMock.Database.EnsureDeleted();

            contextMock.Users.AddRange(
                UserMocks.UserMockToCreate(),
                UserMocks.UserMockToCreate(),
                UserMocks.UserMockToCreate(),
                UserMocks.UserMockToCreate(),
                UserMocks.UserMockToCreate()
            );

            contextMock.SaveChanges();

            userRepositoryMock = new UserRepository(contextMock);
        }

        [Fact]
        public async void should_has_user_with_id_successfully()
        {
            Assert.True(await userRepositoryMock.hasUserWithId(1));
        }

        [Fact]
        public async void should_has_user_with_id_when_false_successfully()
        {
            Assert.False(await userRepositoryMock.hasUserWithId(99));
        }

        [Fact]
        public async void should_insert_successfully()
        {
            var myException = await Record.ExceptionAsync(() => userRepositoryMock.Insert(UserMocks.UserMockToCreate()));
            Assert.Null(myException);
        }

        [Fact]
        public async void should_insert_range_successfully()
        {
            var myException = await Record.ExceptionAsync(() => userRepositoryMock.Insert(
                new List<User> { UserMocks.UserMockToCreate(), UserMocks.UserMockToCreate() }));
            Assert.Null(myException);
        }

        [Fact]
        public async void should_get_all_async_successfully()
        {
            Assert.NotNull(await userRepositoryMock.GetAllAsync());
        }

        [Fact]
        public async void should_get_by_id_successfully()
        {
            Assert.NotNull(await userRepositoryMock.GetByIdAsync(1));
        }

        [Fact]
        public async void should_delete_successfully()
        {
            var user = await userRepositoryMock.GetByIdAsync(5);
            var myException = Record.Exception(() => userRepositoryMock.Delete(UserMocks.UserMockWithId(5)));
            Assert.Null(myException);
        }

        [Fact]
        public void should_get_database_successfully()
        {
            Assert.NotNull(userRepositoryMock.GetDatabase());
        }

        [Fact]
        public void should_include_successfully()
        {
            Assert.NotNull(userRepositoryMock.Include());
        }

        [Fact]
        public void should_get_queryable_successfully()
        {
            Assert.NotNull(userRepositoryMock.GetQueryable());
        }

        public void Dispose()
        {
        }
    }
}
