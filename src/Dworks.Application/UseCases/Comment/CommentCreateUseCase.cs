using Dworks.Application.Interfaces.UseCases;
using DWorks.Domain.Exceptions;
using DWorks.Domain.Interfaces.Repositories;

namespace Dworks.Application.UseCases.Comment
{
    public class CommentCreateUseCase : ICommentCreateUseCase
    {
        private readonly ICommentRepository _commentRepository;

        public CommentCreateUseCase(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async System.Threading.Tasks.Task execute(DWorks.Domain.Entities.Comment comment)
        {
            try
            {
                await _commentRepository.Insert(comment);
            }
            catch (Exception e)
            {
                throw new BadRequestException(e.Message);
            }
        }
    }
}
