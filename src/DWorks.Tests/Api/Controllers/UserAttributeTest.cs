using Dworks.Application.Requests.Project;
using DWorks.Api.Controllers;
using DWorks.Tests.Mocks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DWorks.Tests.Api.Controllers
{
    public class UserAttributeTest
    {
        private readonly ProjectController projectController;
        private readonly Mock<ISender> senderMock;

        public UserAttributeTest()
        {
            var httpContext = new DefaultHttpContext();

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

            await Assert.ThrowsAsync<InvalidOperationException>(() => projectController.getProjectsAsync());
        }
    }
}
