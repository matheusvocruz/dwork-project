using Dworks.Application.Requests.Project;
using Dworks.Application.Requests.Task;
using DWorks.Api.Controllers;
using DWorks.Tests.Mocks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DWorks.Tests.Api.Controllers
{
    public class ProjectControllerTest
    {
        private readonly ProjectController projectController;
        private readonly Mock<ISender> senderMock;

        public ProjectControllerTest()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["x-user-id"] = "1";

            senderMock = new Mock<ISender>();
            projectController = new ProjectController(senderMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };
        }

        [Fact]
        public async void should_get_project_async_successfully()
        {
            senderMock.Setup(s => s.Send(It.IsAny<GetProjectsRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ProjectMocks.ApiResponseGetProjectsResponseMock());
           
            var result = await projectController.getProjectsAsync();
            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
        }

        [Fact]
        public async void should_get_tasks_by_project_async_successfully()
        {
            senderMock.Setup(s => s.Send(It.IsAny<GetTasksByProjectRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(TaskMocks.ApiResponseGetTasksResponseMock());

            var request = TaskMocks.GetTasksByProjectRequestMock();
            var result = await projectController.getTasksByProjectAsync(request);
            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void should_create_async_successfully()
        {
            senderMock.Setup(s => s.Send(It.IsAny<CreateProjectRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ProjectMocks.ApiResponseCreateProjectResponseMock());

            var request = ProjectMocks.CreateProjectRequestMock();
            var result = await projectController.createAsync(request);
            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void should_delete_async_successfully()
        {
            senderMock.Setup(s => s.Send(It.IsAny<DeleteProjectRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ProjectMocks.ApiResponseUnitResponseMock());

            var request = ProjectMocks.DeleteProjectRequestMock();
            var result = await projectController.deleteAsync(request);
            var noContentResult = result as NoContentResult;

            Assert.Null(noContentResult);
        }
    }
}
