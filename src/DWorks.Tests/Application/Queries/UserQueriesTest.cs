using Dworks.Application.Queries;
using DWorks.Domain.Exceptions;
using DWorks.Domain.Interfaces.Repositories;
using DWorks.Tests.Mocks;
using Moq;

namespace DWorks.Tests.Application.Queries
{
    public class UserQueriesTest
    {
        private readonly UserQueries userQueries;
        private readonly Mock<IUserRepository> userRepositoryMock;

        public UserQueriesTest()
        {
            userRepositoryMock = new Mock<IUserRepository>();
            userQueries = new UserQueries(userRepositoryMock.Object);
        }

        [Fact]
        public async void should_get_by_id_successfully()
        {
            userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(UserMocks.UserMock());

            var result = await userQueries.getById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async void should_get_by_id_throw_bad_request_when_unexpected_throw()
        {
            userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>())).ThrowsAsync(new Exception("Lock"));
            await Assert.ThrowsAsync<BadRequestException>(() => userQueries.getById(1));
        }

        [Fact]
        public async void should_has_user_with_id_successfully()
        {
            userRepositoryMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(true);

            var result = await userQueries.hasUserWithId(1);

            Assert.True(result);
        }

        [Fact]
        public async void should_has_user_with_id_throw_bad_request_when_unexpected_throw()
        {
            userRepositoryMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ThrowsAsync(new Exception("Lock"));
            await Assert.ThrowsAsync<BadRequestException>(() => userQueries.hasUserWithId(1));
        }
    }
}
