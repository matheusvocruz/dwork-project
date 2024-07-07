﻿using AutoMapper;
using Dworks.Application.Commands.TaskCommands;
using Dworks.Application.Interfaces.Queries;
using Dworks.Application.Interfaces.UseCases;
using Dworks.Application.Mapper;
using DWorks.Tests.Mocks;
using Moq;
using System.Net;

namespace DWorks.Tests.Application.Commands.TaskCommands
{
    public class CreateTaskCommandHandlerTest
    {
        private readonly CreateTaskCommandHandler createTaskCommandHandlerTest;
        private readonly Mock<IProjectQueries> projectQueriesMock;
        private readonly Mock<ITaskQueries> taskQueriesMock;
        private readonly Mock<ITaskCreateUseCase> taskCreateUseCaseMock;
        private readonly Mock<IUserQueries> userQueriesMock;
        private readonly IMapper mapperMock;

        public CreateTaskCommandHandlerTest()
        {
            projectQueriesMock = new Mock<IProjectQueries>();
            taskQueriesMock = new Mock<ITaskQueries>();
            taskCreateUseCaseMock = new Mock<ITaskCreateUseCase>();
            userQueriesMock = new Mock<IUserQueries>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DomainToApplicationMappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            mapperMock = mapper;
            createTaskCommandHandlerTest = new CreateTaskCommandHandler(projectQueriesMock.Object,
                taskQueriesMock.Object, taskCreateUseCaseMock.Object, userQueriesMock.Object, mapperMock);
        }

        [Fact]
        public async void should_handle_successfully()
        {
            userQueriesMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(true);
            projectQueriesMock.Setup(x => x.getById(It.IsAny<long>())).ReturnsAsync(ProjectMocks.ProjectMock());
            taskQueriesMock.Setup(x => x.getCountByProject(It.IsAny<long>())).ReturnsAsync(1);

            var request = TaskMocks.CreateTaskRequestMock();
            var result = await createTaskCommandHandlerTest.Handle(request, new CancellationToken());

            taskCreateUseCaseMock.Verify(x => x.execute(It.IsAny<Domain.Entities.Task>()), Times.AtLeastOnce);

            Assert.True(result.ValidationResult.Errors.Count().Equals(0));
        }

        [Fact]
        public async void should_return_error_not_found_user()
        {
            userQueriesMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(false);

            var request = TaskMocks.CreateTaskRequestMock();
            var result = await createTaskCommandHandlerTest.Handle(request, new CancellationToken());

            Assert.Equal(result.ValidationResult.Errors.First().ErrorCode, HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void should_return_error_not_found_task()
        {
            userQueriesMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(true);
            projectQueriesMock.Setup(x => x.getById(It.IsAny<long>())).ReturnsAsync(() => null);

            var request = TaskMocks.CreateTaskRequestMock();
            var result = await createTaskCommandHandlerTest.Handle(request, new CancellationToken());

            Assert.Equal(result.ValidationResult.Errors.First().ErrorCode, HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void should_return_error_bad_request_pending_tasks()
        {
            userQueriesMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(true);
            projectQueriesMock.Setup(x => x.getById(It.IsAny<long>())).ReturnsAsync(ProjectMocks.ProjectMock());
            taskQueriesMock.Setup(x => x.getCountByProject(It.IsAny<long>())).ReturnsAsync(20);

            var request = TaskMocks.CreateTaskRequestMock();
            var result = await createTaskCommandHandlerTest.Handle(request, new CancellationToken());

            Assert.Equal(result.ValidationResult.Errors.First().ErrorCode, HttpStatusCode.BadRequest.ToString());
        }

        [Fact]
        public async void should_return_error_bad_request_unexpected_throw()
        {
            userQueriesMock.Setup(x => x.hasUserWithId(It.IsAny<long>())).ReturnsAsync(true);
            projectQueriesMock.Setup(x => x.getById(It.IsAny<long>())).ThrowsAsync(new Exception("Lock"));

            var request = TaskMocks.CreateTaskRequestMock();
            var result = await createTaskCommandHandlerTest.Handle(request, new CancellationToken());

            Assert.Equal(result.ValidationResult.Errors.First().ErrorCode, HttpStatusCode.BadRequest.ToString());
        }
    }
}
