using Dworks.Application.Responses.ApiResponse;
using Dworks.Application.Responses.Project;
using DWorks.Domain.Entities;

namespace Dworks.Application.Interfaces.Queries
{
    public interface IProjectQueries
    {
        Task<Project> getById(long id);
        Task<GetProjectsResponse> getAll();
    }
}
