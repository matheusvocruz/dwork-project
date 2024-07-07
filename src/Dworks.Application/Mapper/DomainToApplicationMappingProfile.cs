using AutoMapper;
using Dworks.Application.Responses.Project;
using Dworks.Application.Responses.Report;
using Dworks.Application.Responses.Task;
using DWorks.Domain.Entities;
using DWorks.Domain.ValueObjects;

namespace Dworks.Application.Mapper
{
    public class DomainToApplicationMappingProfile : Profile
    {
        public DomainToApplicationMappingProfile()
        {
            CreateMap<Project, GetProjectResponse>();
            CreateMap<DWorks.Domain.Entities.Task, GetTaskResponse>();
            CreateMap<DoneTaskDto, DoneTask>();
        }
    }
}
