using AutoMapper;
using Dworks.Application.Interfaces.Queries;
using Dworks.Application.Interfaces.UseCases;
using Dworks.Application.Requests.Task;
using Dworks.Application.Responses.ApiResponse;
using Dworks.Application.Responses.Task;
using DWorks.Domain.Enums.Task;
using DWorks.Domain.Exceptions;
using MediatR;

namespace Dworks.Application.Commands.TaskCommands
{
    public class CreateTaskCommandHandler : CommandHandler<CreateTaskResponse>, IRequestHandler<CreateTaskRequest, ApiResponse<CreateTaskResponse>>
    {
        private readonly IProjectQueries _projectQueries;
        private readonly ITaskQueries _taskQueries;
        private readonly ITaskCreateUseCase _taskCreateUseCase;
        private readonly IUserQueries _userQueries;
        private readonly IMapper _mapper;

        public CreateTaskCommandHandler(
            IProjectQueries projectQueries, 
            ITaskQueries taskQueries,
            ITaskCreateUseCase taskCreateUseCase,
            IUserQueries userQueries,
            IMapper mapper)
        {
            _projectQueries = projectQueries;
            _taskQueries = taskQueries;
            _taskCreateUseCase = taskCreateUseCase;
            _userQueries = userQueries;
            _mapper = mapper;
        }

        public async Task<ApiResponse<CreateTaskResponse>> Handle(CreateTaskRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _userQueries.hasUserWithId(request.UserId))
                    throw new NotFoundException("User doesn't exists");

                var project = await _projectQueries.getById(request.ProjectId) ?? throw new NotFoundException("Projeto não encontrato");

                if (await _taskQueries.getCountByProject(request.ProjectId) >= 20)
                    throw new BadRequestException("Número máximo de tasks por projeto");

                var task = new DWorks.Domain.Entities.Task
                {
                    Tittle = request.Tittle,
                    Description = request.Description,
                    CreatedAt = DateTime.Now,
                    Status = TaskStatusEnum.PENDING,
                    Priority = request.Priority,
                    ProjectId = request.ProjectId,
                    ExpiresAt = request.ExpiresAt
                };

                await _taskCreateUseCase.execute(task);

                Response.Data = _mapper.Map<GetTaskResponse>(task);
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
