using Dworks.Application.Interfaces.Queries;
using Dworks.Application.Interfaces.UseCases;
using Dworks.Application.Requests.Task;
using Dworks.Application.Responses.ApiResponse;
using DWorks.Domain.Exceptions;
using DWorks.Domain.Interfaces.Repositories;
using MediatR;

namespace Dworks.Application.Commands.TaskCommands
{
    public class DeleteTaskCommandHandler : CommandHandler<UnitResponse>, IRequestHandler<DeleteTaskRequest, ApiResponse<UnitResponse>>
    {
        private readonly ITaskDeleteUseCase _taskDeleteUseCase;
        private readonly ITaskQueries _taskQueries;
        private readonly IUserQueries _userQueries;

        public DeleteTaskCommandHandler(
            ITaskDeleteUseCase taskDeleteUseCase, 
            ITaskQueries taskQueries, 
            IUserQueries userQueries)
        {
            _taskDeleteUseCase = taskDeleteUseCase;
            _taskQueries = taskQueries;
            _userQueries = userQueries;
        }

        public async Task<ApiResponse<UnitResponse>> Handle(DeleteTaskRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _userQueries.hasUserWithId(request.UserId))
                    throw new NotFoundException("User doesn't exists");

                var task = await _taskQueries.getById(request.Id) ?? throw new NotFoundException("Task não encontrada");

                _taskDeleteUseCase.execute(task);

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
    }
}
