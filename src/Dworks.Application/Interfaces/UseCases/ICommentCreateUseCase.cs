using DWorks.Domain.Entities;

namespace Dworks.Application.Interfaces.UseCases
{
    public interface ICommentCreateUseCase
    {
        System.Threading.Tasks.Task execute(Comment comment);
    }
}
