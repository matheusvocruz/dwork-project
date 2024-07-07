using AutoMapper;
using Dworks.Application.Interfaces.Queries;
using Dworks.Application.Responses.Project;
using DWorks.Domain.Entities;
using DWorks.Domain.Exceptions;
using DWorks.Domain.Interfaces.Repositories;

namespace Dworks.Application.Queries
{
    public class ProjectQueries : IProjectQueries
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectQueries(IProjectRepository projectRepository, IMapper mapper) {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<Project> getById(long id)
        {
            try
            {
                return await _projectRepository.GetByIdAsync(id);
            }
            catch (Exception e)
            {
                throw new BadRequestException(e.Message);
            }
        }

        public async Task<GetProjectsResponse> getAll()
        {
            try
            {
                return new GetProjectsResponse { Projects = _mapper.Map<List<GetProjectResponse>>(await _projectRepository.GetAllAsync()) };
            }
            catch (Exception e)
            {
                throw new BadRequestException(e.Message);
            }
        }
    }
}
