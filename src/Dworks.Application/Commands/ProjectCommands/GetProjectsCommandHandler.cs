using Dworks.Application.Interfaces.Queries;
using Dworks.Application.Requests.Project;
using Dworks.Application.Responses.ApiResponse;
using Dworks.Application.Responses.Project;
using DWorks.Domain.Exceptions;
using MediatR;

namespace Dworks.Application.Commands.ProjectCommands
{
    public class GetProjectsCommandHandler : CommandHandler<GetProjectsResponse>, IRequestHandler<GetProjectsRequest, ApiResponse<GetProjectsResponse>>
    {
        private readonly IProjectQueries _projectQueries;
        private readonly IUserQueries _userQueries;

        public GetProjectsCommandHandler(IProjectQueries projectQueries, IUserQueries userQueries)
        {
            _projectQueries = projectQueries;
            _userQueries = userQueries;
        }

        public async Task<ApiResponse<GetProjectsResponse>> Handle(GetProjectsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _userQueries.hasUserWithId(request.UserId))
                    throw new NotFoundException("User doesn't exists");

                var response = await _projectQueries.getAll();

                Response.Data = response;

                return Response;
            }
            catch (NotFoundException notFoundException)
            {
                return ThrowError(notFoundException);
            }
            catch (Exception e) 
            {
                return ThrowError(new BadRequestException(e.Message));
            }
        }
    }
}
