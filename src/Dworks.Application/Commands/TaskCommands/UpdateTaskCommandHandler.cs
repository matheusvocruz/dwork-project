using Dworks.Application.Interfaces.Queries;
using Dworks.Application.Interfaces.UseCases;
using Dworks.Application.Requests.Task;
using Dworks.Application.Responses.ApiResponse;
using DWorks.Domain.Entities;
using DWorks.Domain.Exceptions;
using MediatR;

namespace Dworks.Application.Commands.TaskCommands
{
    public class UpdateTaskCommandHandler : CommandHandler<UnitResponse>, IRequestHandler<UpdateTaskRequest, ApiResponse<UnitResponse>>
    {
        private readonly ITaskUpdateUseCase _taskUpdateUseCase;
        private readonly ITaskQueries _taskQueries;
        private readonly ILogCreateUseCase _logCreateUseCase;
        private readonly IUserQueries _userQueries;

        public UpdateTaskCommandHandler(
            ITaskUpdateUseCase taskUpdateUseCase, 
            ITaskQueries taskQueries,
            ILogCreateUseCase logCreateUseCase,
            IUserQueries userQueries)
        {
            _taskUpdateUseCase = taskUpdateUseCase;
            _taskQueries = taskQueries;
            _logCreateUseCase = logCreateUseCase;
            _userQueries = userQueries;
        }

        public async Task<ApiResponse<UnitResponse>> Handle(UpdateTaskRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _userQueries.hasUserWithId(request.UserId))
                    throw new NotFoundException("User doesn't exists");

                var task = await _taskQueries.getById(request.Id) ?? throw new NotFoundException("Task não encontrada");

                List<Log> logs = new();

                if (!request.Status.Equals(task.Status))
                {
                    logs.Add(buildLog(task.Id, nameof(task.Status), task.Status.ToString(), request.Status.ToString()));
                    task.Status = request.Status;
                }

                if (!request.Tittle.Equals(task.Tittle))
                {
                    logs.Add(buildLog(task.Id, nameof(task.Tittle), task.Tittle, request.Tittle));
                    task.Tittle = request.Tittle;
                }

                if (!request.Description.Equals(task.Description))
                {
                    logs.Add(buildLog(task.Id, nameof(task.Description), task.Description, request.Description));
                    task.Description = request.Description;
                }

                if (!request.ExpiresAt.Equals(task.ExpiresAt))
                {
                    logs.Add(buildLog(task.Id, nameof(task.ExpiresAt), task.ExpiresAt.ToString(), request.ExpiresAt.ToString()));
                    task.ExpiresAt = request.ExpiresAt;
                }

                if (logs.Count > 0)
                {
                    await _taskUpdateUseCase.execute(task);
                    await _logCreateUseCase.execute(logs);
                }

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

        private Log buildLog(long id, string field, string oldValue, string newValue)
            => new Log { CreatedAt = DateTime.Now, Entity = "Task", Operation = "Update", 
                EntityId = id, Field = field, Old = oldValue, New = newValue };
    }
}
