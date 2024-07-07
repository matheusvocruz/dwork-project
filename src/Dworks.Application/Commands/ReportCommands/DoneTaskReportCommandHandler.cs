using Dworks.Application.Interfaces.Queries;
using Dworks.Application.Requests.Report;
using Dworks.Application.Responses.ApiResponse;
using Dworks.Application.Responses.Report;
using DWorks.Domain.Enums.User;
using DWorks.Domain.Exceptions;
using MediatR;

namespace Dworks.Application.Commands.ReportCommands
{
    public class DoneTaskReportCommandHandler : CommandHandler<DoneTaskReportResponse>, IRequestHandler<DoneTaskReportRequest, ApiResponse<DoneTaskReportResponse>>
    {
        private readonly ITaskQueries _taskQueries;
        private readonly IUserQueries _userQueries;

        public DoneTaskReportCommandHandler(
            ITaskQueries taskQueries,
            IUserQueries userQueries)
        {
            _taskQueries = taskQueries;
            _userQueries = userQueries;
        }

        public async Task<ApiResponse<DoneTaskReportResponse>> Handle(DoneTaskReportRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userQueries.getById(request.UserId);

                if (user is null)
                    throw new NotFoundException("User doesn't exists");

                if (!user.Type.Equals(UserTypeEnum.Manager))
                    throw new NotAllowedException("User not allowed to get reports");

                Response.Data = await _taskQueries.getDoneReport();
                
                return Response;
            }
            catch (NotFoundException e)
            {
                return ThrowError(new NotFoundException(e.Message));
            }
            catch (NotAllowedException e)
            {
                return ThrowError(new NotAllowedException(e.Message));
            }
            catch (Exception e)
            {
                return ThrowError(new BadRequestException(e.Message));
            }
        }
    }
}
