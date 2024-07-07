using AutoMapper;
using Dworks.Application.Interfaces.Queries;
using Dworks.Application.Mapper;
using Dworks.Application.Queries;
using Dworks.Application.Responses.Project;
using DWorks.Domain.Entities;
using DWorks.Domain.Exceptions;
using DWorks.Domain.Interfaces.Repositories;
using DWorks.Tests.Mocks;
using Moq;

namespace DWorks.Tests.Application.Queries
{
    public class ProjectQueriesTest
    {
        private readonly ProjectQueries projectQueries;
        private readonly Mock<IProjectRepository> projectRepositoryMock;
        private readonly IMapper mapperMock;

        public ProjectQueriesTest()
        {
            projectRepositoryMock = new Mock<IProjectRepository>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
               mc.AddProfile(new DomainToApplicationMappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            mapperMock = mapper;
            projectQueries = new ProjectQueries(projectRepositoryMock.Object, mapperMock);
        }

        [Fact]
        public async void should_get_by_id_successfully()
        {
            projectRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(ProjectMocks.ProjectMock());

            var result = await projectQueries.getById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async void should_get_by_id_throw_bad_request_when_unexpected_throw()
        {
            projectRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>())).ThrowsAsync(new Exception("Lock"));
            await Assert.ThrowsAsync<BadRequestException>(() => projectQueries.getById(1));
        }

        [Fact]
        public async void should_get_all_successfully()
        {
            projectRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(ProjectMocks.ProjectListMock());

            var result = await projectQueries.getAll();

            Assert.NotNull(result);
            Assert.Equal(1, result.Projects.First().Id);
        }

        [Fact]
        public async void should_get_all_throw_bad_request_when_unexpected_throw()
        {
            projectRepositoryMock.Setup(x => x.GetAllAsync()).ThrowsAsync(new Exception("Lock"));
            await Assert.ThrowsAsync<BadRequestException>(() => projectQueries.getAll());
        }
    }
}
