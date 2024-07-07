using Dworks.Application.Requests.Task;
using DWorks.Domain.Entities;

namespace DWorks.Tests.Mocks
{
    public static class CommentMocks
    {
        public static CreateTaskCommentRequest CreateTaskCommentRequestMock()
            => new CreateTaskCommentRequest { Comment = "Comment", TaskId = 1, UserId = 1 };

        public static Comment CommentMock()
            => new Comment { Content = "Content", Id = 1, TaskId = 1 };
    }
}
