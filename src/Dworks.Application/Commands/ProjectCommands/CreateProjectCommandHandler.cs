using AutoMapper;
using Dworks.Application.Interfaces.Queries;
using Dworks.Application.Interfaces.UseCases;
using Dworks.Application.Requests.Project;
using Dworks.Application.Responses.ApiResponse;
using Dworks.Application.Responses.Project;
using DWorks.Domain.Entities;
using DWorks.Domain.Exceptions;
using MediatR;

namespace Dworks.Application.Commands.ProjectCommands
{
    public class CreateProjectCommandHandler : CommandHandler<CreateProjectResponse>, IRequestHandler<CreateProjectRequest, ApiResponse<CreateProjectResponse>>
    {
        private readonly IProjectCreateUseCase _projectCreateUseCase;
        private readonly IUserQueries _userQueries;
        private readonly IMapper _mapper;

        public CreateProjectCommandHandler(
            IProjectCreateUseCase projectCreateUseCase,
            IUserQueries userQueries,
            IMapper mapper)
        {
            _projectCreateUseCase = projectCreateUseCase;
            _userQueries = userQueries;
            _mapper = mapper;
        }

        public async Task<ApiResponse<CreateProjectResponse>> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (! await _userQueries.hasUserWithId(request.UserId))
                    throw new NotFoundException("User doesn't exists");

                var project = new Project { 
                    Name = request.Name, 
                    CreatedAt = DateTime.Now,
                    UserId = request.UserId
                };

                await _projectCreateUseCase.execute(project);

                Response.Data = _mapper.Map<GetProjectResponse>(project);

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
