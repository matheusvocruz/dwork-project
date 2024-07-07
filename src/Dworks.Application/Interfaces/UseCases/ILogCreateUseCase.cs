using DWorks.Domain.Entities;

namespace Dworks.Application.Interfaces.UseCases
{
    public interface ILogCreateUseCase
    {
        System.Threading.Tasks.Task execute(List<Log> log);
    }
}
