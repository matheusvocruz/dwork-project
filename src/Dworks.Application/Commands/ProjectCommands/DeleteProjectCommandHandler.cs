using Dworks.Application.Interfaces.Queries;
using Dworks.Application.Interfaces.UseCases;
using Dworks.Application.Requests.Project;
using Dworks.Application.Responses.ApiResponse;
using DWorks.Domain.Exceptions;
using MediatR;

namespace Dworks.Application.Commands.ProjectCommands
{
    public class DeleteProjectCommandHandler : CommandHandler<UnitResponse>, IRequestHandler<DeleteProjectRequest, ApiResponse<UnitResponse>>
    {
        private readonly IProjectQueries _projectQueries;
        private readonly ITaskQueries _taskQueries;
        private readonly IProjectDeleteUseCase _projectDeleteUseCase;
        private readonly IUserQueries _userQueries;

        public DeleteProjectCommandHandler(
            IProjectQueries projectQueries,
            ITaskQueries taskQueries,
            IProjectDeleteUseCase projectDeleteUseCase,
            IUserQueries userQueries)
        {
            _projectQueries = projectQueries;
            _taskQueries = taskQueries;
            _projectDeleteUseCase = projectDeleteUseCase;
            _userQueries = userQueries;
        }

        public async Task<ApiResponse<UnitResponse>> Handle(DeleteProjectRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _userQueries.hasUserWithId(request.UserId))
                    throw new NotFoundException("User doesn't exists");

                var task = await _projectQueries.getById(request.Id) ?? throw new NotFoundException("Projeto não encontrado");
                
                if (await _taskQueries.hasPendingTaskByProject(request.Id))
                    throw new BadRequestException("Existem tarefas pendentes nesse projeto, não é possível remover");

                _projectDeleteUseCase.execute(task);

                return Response;
            }
            catch (NotFoundException notFoundException)
            {
                return ThrowError(notFoundException);
            }
            catch (Exception ex)
            {
                return ThrowError(new BadRequestException(ex.Message));
            }
        }
    }
}
