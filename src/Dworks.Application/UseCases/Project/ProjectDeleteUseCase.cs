using Dworks.Application.Interfaces.UseCases;
using DWorks.Domain.Exceptions;
using DWorks.Domain.Interfaces.Repositories;

namespace Dworks.Application.UseCases.Project
{
    public class ProjectDeleteUseCase : IProjectDeleteUseCase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectDeleteUseCase(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void execute(DWorks.Domain.Entities.Project project)
        {
            try
            {
                _projectRepository.Delete(project);
            }
            catch (Exception e)
            {
                throw new BadRequestException(e.Message);
            }
        }
    }
}
