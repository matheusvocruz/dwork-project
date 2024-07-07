using DWorks.Api.Controllers;
using MediatR;
using Moq;
using Dworks.Application.Requests.Task;
using DWorks.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace DWorks.Tests.Api.Controllers
{
    public class TaskControllerTest
    {
        private readonly TaskController projectController;
        private readonly Mock<ISender> senderMock;

        public TaskControllerTest()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["x-user-id"] = "1";

            senderMock = new Mock<ISender>();
            projectController = new TaskController(senderMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };
        }

        [Fact]
        public async void should_create_async_successfully()
        {
            senderMock.Setup(s => s.Send(It.IsAny<CreateTaskRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(TaskMocks.ApiResponseGetProjectsResponseMock());

            var request = TaskMocks.CreateTaskRequestMock();
            var result = await projectController.createAsync(request);
            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void should_update_async_successfully()
        {
            senderMock.Setup(s => s.Send(It.IsAny<UpdateTaskRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(TaskMocks.ApiResponseUnitResponseMock());

            var request = TaskMocks.UpdateTaskRequestMock();
            var result = await projectController.updateAsync(request);
            var noContentResult = result as NoContentResult;

            Assert.Null(noContentResult);
        }

        [Fact]
        public async void should_create_comment_async_successfully()
        {
            senderMock.Setup(s => s.Send(It.IsAny<CreateTaskCommentRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(TaskMocks.ApiResponseUnitResponseMock());

            var request = TaskMocks.CreateTaskCommentRequestMock();
            var result = await projectController.createCommentAsync(request);
            var noContentResult = result as NoContentResult;

            Assert.Null(noContentResult);
        }

        [Fact]
        public async void should_delete_async_successfully()
        {
            senderMock.Setup(s => s.Send(It.IsAny<DeleteTaskRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(TaskMocks.ApiResponseUnitResponseMock());

            var request = TaskMocks.DeleteTaskRequestMock();
            var result = await projectController.deleteAsync(request);
            var noContentResult = result as NoContentResult;

            Assert.Null(noContentResult);
        }
    }
}
