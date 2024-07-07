using Dworks.Application.Interfaces.Queries;
using Dworks.Application.Requests.Task;
using Dworks.Application.Responses.ApiResponse;
using Dworks.Application.Responses.Task;
using DWorks.Domain.Exceptions;
using MediatR;

namespace Dworks.Application.Commands.ProjectCommands
{
    public class GetTasksByProjectCommandHandler : CommandHandler<GetTasksResponse>, IRequestHandler<GetTasksByProjectRequest, ApiResponse<GetTasksResponse>>
    {
        private readonly ITaskQueries _taskQueries;
        private readonly IUserQueries _userQueries;

        public GetTasksByProjectCommandHandler(ITaskQueries taskQueries, IUserQueries userQueries)
        {
            _taskQueries = taskQueries;
            _userQueries = userQueries;
        }

        public async Task<ApiResponse<GetTasksResponse>> Handle(GetTasksByProjectRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _userQueries.hasUserWithId(request.UserId))
                    throw new NotFoundException("User doesn't exists");

                var response = await _taskQueries.getAllByProject(request.Id);

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
