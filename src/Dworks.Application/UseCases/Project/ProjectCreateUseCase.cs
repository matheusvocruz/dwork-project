using Dworks.Application.Interfaces.UseCases;
using DWorks.Domain.Exceptions;
using DWorks.Domain.Interfaces.Repositories;

namespace Dworks.Application.UseCases.Project
{
    public class ProjectCreateUseCase : IProjectCreateUseCase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectCreateUseCase(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<DWorks.Domain.Entities.Project> execute(DWorks.Domain.Entities.Project project)
        {
            try
            {
                await _projectRepository.Insert(project);
                return project;
            }
            catch (Exception e)
            {
                throw new BadRequestException(e.Message);
            }
        }
    }
}
