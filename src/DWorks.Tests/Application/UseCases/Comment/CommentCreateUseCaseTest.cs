using Dworks.Application.UseCases.Comment;
using DWorks.Domain.Exceptions;
using DWorks.Domain.Interfaces.Repositories;
using DWorks.Tests.Mocks;
using Moq;

namespace DWorks.Tests.Application.UseCases.Comment
{
    public class CommentCreateUseCaseTest
    {
        private readonly CommentCreateUseCase commentCreateUseCaseMock;
        private readonly Mock<ICommentRepository> commentRepositoryMock;

        public CommentCreateUseCaseTest()
        {
            commentRepositoryMock = new Mock<ICommentRepository>();
            commentCreateUseCaseMock = new CommentCreateUseCase(commentRepositoryMock.Object);
        }

        [Fact]
        public async void should_execute_successfully()
        {
            commentRepositoryMock.Setup(x => x.Insert(It.IsAny<Domain.Entities.Comment>()))
                .Returns(System.Threading.Tasks.Task.FromResult<Domain.Entities.Comment>(null));
            
            var request = CommentMocks.CommentMock();
            var myException = Record.ExceptionAsync(async () => await commentCreateUseCaseMock.execute(request));

            Assert.Null(myException.Result);
        }

        [Fact]
        public async void should_throw_unexpected_and_throw_bad_request_exception()
        {
            commentRepositoryMock.Setup(x => x.Insert(It.IsAny<Domain.Entities.Comment>())).ThrowsAsync(new Exception("Error"));
            var request = CommentMocks.CommentMock();
            await Assert.ThrowsAsync<BadRequestException>(() => commentCreateUseCaseMock.execute(request));
        }
    }
}
