using Dworks.Application.Requests.Task;
using Dworks.Application.Responses.ApiResponse;
using Dworks.Application.Responses.Task;
using DWorks.Domain.Enums.Task;
using FluentValidation.Results;

namespace DWorks.Tests.Mocks
{
    public static class TaskMocks
    {
        public static ApiResponse<GetTasksResponse> ApiResponseGetTasksResponseMock()
        {
            ValidationResult validationResult = new ValidationResult();
            validationResult.Errors = new List<ValidationFailure> { };

            return new ApiResponse<GetTasksResponse>
                {
                    Data = GetTasksResponseMock(),
                    ValidationResult = validationResult
                };
        }

        public static ApiResponse<CreateTaskResponse> ApiResponseGetProjectsResponseMock()
        {
            ValidationResult validationResult = new ValidationResult();
            validationResult.Errors = new List<ValidationFailure> { };

            return new ApiResponse<CreateTaskResponse>
            {
                Data = CreateTaskResponseMock(),
                ValidationResult = validationResult
            };
        }

        public static ApiResponse<UnitResponse> ApiResponseUnitResponseMock()
        {
            ValidationResult validationResult = new ValidationResult();
            validationResult.Errors = new List<ValidationFailure> { };

            return new ApiResponse<UnitResponse>
            {
                ValidationResult = validationResult
            };
        }


        private static GetTasksResponse GetTasksResponseMock()
            => new GetTasksResponse { Tasks = new List<GetTaskResponse> { GetTaskResponseMock() } };

        private static GetTaskResponse GetTaskResponseMock()
            => new GetTaskResponse { Id = 1L, Description = "Description", Priority = TaskPriorityEnum.LOW.ToString(), 
                Status = TaskStatusEnum.PENDING.ToString(), Tittle = "Tittle"};

        private static CreateTaskResponse CreateTaskResponseMock()
            => new CreateTaskResponse { Id = 1L };

        public static CreateTaskRequest CreateTaskRequestMock()
            => new CreateTaskRequest { Description = "description", Tittle = "Tittle", Priority = TaskPriorityEnum.LOW,
                ProjectId = 1L, UserId = 1L, ExpiresAt = DateTime.Now };

        public static UpdateTaskRequest UpdateTaskRequestMock()
            => new UpdateTaskRequest { Id = 1L, Description = "Description", Status = TaskStatusEnum.DONE, UserId = 1L,
                ExpiresAt = DateTime.Now, Tittle = "Tittle"};

        public static CreateTaskCommentRequest CreateTaskCommentRequestMock()
            => new CreateTaskCommentRequest { TaskId = 1L, Comment = "Content" };

        public static UpdateTaskRequest UpdateTaskRequestWithoutUpdateMock()
            => new UpdateTaskRequest { Id = 1L, Description = "Description", Status = TaskStatusEnum.PENDING, UserId = 1L,
                ExpiresAt = DateTime.Now, Tittle = "Tittle"};

        public static DeleteTaskRequest DeleteTaskRequestMock()
            => new DeleteTaskRequest { Id = 1L, UserId = 1L };

        public static Domain.Entities.Task TaskMock()
            => new Domain.Entities.Task { Id = 1L, Description = "Description", Tittle = "Tittle", Priority = TaskPriorityEnum.LOW,
                Status = TaskStatusEnum.PENDING, ProjectId = 1L, CreatedAt = DateTime.Now };

        public static List<Domain.Entities.Task> TaskListMock()
            => new List<Domain.Entities.Task> { TaskMock() };

        public static GetTasksByProjectRequest GetTasksByProjectRequestMock()
            => new GetTasksByProjectRequest { Id = 1L, UserId = 1L };

        public static Domain.Entities.Task TaskCreateMock()
            => new Domain.Entities.Task { Description = "Description", Tittle = "Tittle", Priority = TaskPriorityEnum.LOW,
                Status = TaskStatusEnum.PENDING, ProjectId = 1L, CreatedAt = DateTime.Now };

    }
}
