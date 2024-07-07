using Dworks.Application.Requests.Project;
using Dworks.Application.Responses.ApiResponse;
using Dworks.Application.Responses.Project;
using DWorks.Domain.Entities;
using FluentValidation.Results;

namespace DWorks.Tests.Mocks
{
    public static class ProjectMocks
    {
        public static ApiResponse<GetProjectsResponse> ApiResponseGetProjectsResponseMock()
        {
            ValidationResult validationResult = new ValidationResult();
            validationResult.Errors = new List<ValidationFailure> { };

            return new ApiResponse<GetProjectsResponse>
                {
                    Data = GetProjectsResponseMock(),
                    ValidationResult = validationResult
                };
        }

        public static ApiResponse<CreateProjectResponse> ApiResponseCreateProjectResponseMock()
        {
            ValidationResult validationResult = new ValidationResult();
            validationResult.Errors = new List<ValidationFailure> { };

            return new ApiResponse<CreateProjectResponse>
            {
                Data = CreateProjectResponseMock(),
                ValidationResult = validationResult
            };
        }

        public static ApiResponse<UnitResponse> ApiResponseUnitResponseMock()
        {
            ValidationResult validationResult = new ValidationResult();
            validationResult.Errors = new List<ValidationFailure> { };

            return new ApiResponse<UnitResponse>
            {
                Data = DeleteProjectRequestMock(),
                ValidationResult = validationResult
            };
        }

        public static GetProjectsResponse GetProjectsResponseMock()
            => new GetProjectsResponse { Projects = new List<GetProjectResponse> { GetProjectResponseMock() } };

        public static List<GetProjectResponse> GetProjectResponseLickMock()
            => new List<GetProjectResponse> { GetProjectResponseMock() };

        public static GetProjectResponse GetProjectResponseMock()
            => new GetProjectResponse { Id = 1, Name = "Name" };

        public static CreateProjectRequest CreateProjectRequestMock()
            => new CreateProjectRequest { Name = "Name", UserId = 1L };

        private static CreateProjectResponse CreateProjectResponseMock()
            => new CreateProjectResponse { Id = 1L, Name = "Name" };

        public static DeleteProjectRequest DeleteProjectRequestMock()
            => new DeleteProjectRequest { Id = 1L, UserId = 1L };

        public static Project ProjectMock()
            => new Project { Id = 1L, Name = "Name" };

        public static List<Project> ProjectListMock()
            => new List<Project> { ProjectMock() };

        public static GetProjectsRequest GetProjectsRequestMock()
            => new GetProjectsRequest { UserId = 1L };

        public static Project ProjectCreateMock()
            => new Project { Name = "Name", UserId = 1 };
    }
}
