using Moq;
using Dworks.Application.Commands.ProjectCommands;
using DWorks.Tests.Mocks;
using DWorks.Domain.Entities;
using System.Net;
using Dworks.Application.Interfaces.Queries;
using Dworks.Application.Interfaces.UseCases;
using AutoMapper;
using Dworks.Application.Mapper;

namespace DWorks.Tests.Application.Commands.ProjectCommands
{
    public class CreateProjectCommandHandlerTest
    {
        private readonly CreateProjectCommandHandler createProjectCommandHandlerMock;
        private readonly Mock<IProjectCreateUseCase> projectCreateUseCaseMock;
        private readonly Mock<IUserQueries> userQueriesMock;
        private readonly IMapper mapperMock;

        public CreateProjectCommandHandlerTest()
        {
            projectCreateUseCaseMock = new Mock<IProjectCreateUseCase>();
            userQueriesMock = new Mock<IUserQueries>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DomainToApplicationMappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            mapperMock = mapper;
            createProjectCommandHandlerMock = new CreateProjectCommandHandler(projectCreateUseCaseMock.Object, userQueriesMock.Object, mapperMock);
        }

        [Fact]
        public async void should_handle_successfully()
        {
            userQueriesMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(true);

            var request = ProjectMocks.CreateProjectRequestMock();
            var result = await createProjectCommandHandlerMock.Handle(request, new CancellationToken());
            projectCreateUseCaseMock.Verify(x => x.execute(It.IsAny<Project>()), Times.AtLeastOnce);
            
            Assert.True(result.ValidationResult.Errors.Count().Equals(0));
        }

        [Fact]
        public async void should_return_error_not_found_user()
        {
            userQueriesMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(false);

            var request = ProjectMocks.CreateProjectRequestMock();
            var result = await createProjectCommandHandlerMock.Handle(request, new CancellationToken());

            Assert.Equal(result.ValidationResult.Errors.First().ErrorCode, HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void should_throw_unexpected_and_throw_bad_request_exception()
        {
            userQueriesMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(true);

            var request = ProjectMocks.CreateProjectRequestMock();
            projectCreateUseCaseMock.Setup(x => x.execute(It.IsAny<Project>())).ThrowsAsync(new Exception("Incorrect"));
            var result = await createProjectCommandHandlerMock.Handle(request, new CancellationToken());

            Assert.Equal(result.ValidationResult.Errors.First().ErrorCode, HttpStatusCode.BadRequest.ToString());
        }
    }
}
