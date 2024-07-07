using Dworks.Application.Interfaces.Queries;
using Dworks.Application.Interfaces.UseCases;
using Dworks.Application.Requests.Task;
using Dworks.Application.Responses.ApiResponse;
using DWorks.Domain.Entities;
using DWorks.Domain.Exceptions;
using MediatR;

namespace Dworks.Application.Commands.TaskCommands
{
    public class CreateTaskCommentCommandHandler : CommandHandler<UnitResponse>, IRequestHandler<CreateTaskCommentRequest, ApiResponse<UnitResponse>>
    {
        private readonly ICommentCreateUseCase _commentCreateUseCase;
        private readonly ITaskQueries _taskQueries;
        private readonly ILogCreateUseCase _logCreateUseCase;
        private readonly IUserQueries _userQueries;

        public CreateTaskCommentCommandHandler(
            ICommentCreateUseCase commentCreateUseCase,
            ITaskQueries taskQueries,
            ILogCreateUseCase logCreateUseCase,
            IUserQueries userQueries)
        {
            _commentCreateUseCase = commentCreateUseCase;
            _taskQueries = taskQueries;
            _logCreateUseCase = logCreateUseCase;
            _userQueries = userQueries;
        }

        public async Task<ApiResponse<UnitResponse>> Handle(CreateTaskCommentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _userQueries.hasUserWithId(request.UserId))
                    throw new NotFoundException("User doesn't exists");

                var task = await _taskQueries.getById(request.TaskId) ?? throw new NotFoundException("Task não encontrada");

                var comment = new Comment { Content = request.Comment, TaskId = request.TaskId, CreatedAt = DateTime.Now };

                await _commentCreateUseCase.execute(comment);
                await _logCreateUseCase.execute(buildLog(comment.Id, comment.Content, request.TaskId));

                return Response;
            }
            catch (NotFoundException e)
            {
                return ThrowError(new NotFoundException(e.Message));
            }
            catch (Exception e)
            {
                return ThrowError(new BadRequestException(e.Message));
            }
        }

        private List<Log> buildLog(long id, string value, long taskId)
            => new List<Log> {
                new Log
                {
                    CreatedAt = DateTime.Now,
                    Entity = "Commant",
                    Operation = "Insert",
                    EntityId = id,
                    New = value,
                    Parent = "Task",
                    ParentId = taskId
                }
            };
    }
}
